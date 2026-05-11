using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RvasApp.Data;
using RvasApp.Models;
using System.Security.Claims;

namespace RvasApp.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogController(ApplicationDbContext context, UserManager<Korisnik> userManager)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var objave = await _context.Postovi
                .Include(p => p.Korisnik)
                .ToListAsync();

            return View(objave);
        }

        public async Task<IActionResult> Objavi()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Objavi(Post objava)
        {
            var korisnikId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                var p = new Post()
                {
                    Naslov = objava.Naslov,
                    Sadrzaj = objava.Sadrzaj,
                    DatumKreiranja = DateTime.UtcNow,
                    KorisnikId = korisnikId
                };
                _context.Postovi.Add(p);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(objava);
        }


        public async Task<IActionResult> Objava(int? id)
        {
            if (id == null)
                return NotFound();
            var objava = await _context.Postovi
                //kako bi prikazivali autora
                .Include(p => p.Korisnik)
                //kako bi prikazivali i komentare
                .Include(p=>p.Komentari)
                    //i autora komentara
                    .ThenInclude(k=>k.Korisnik)
                .FirstOrDefaultAsync(p => p.PostId == id);

            if (objava == null)
                return NotFound();

            return View(objava);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DodajKomentar(int postId, string sadrzaj)
        {
            if (postId <= 0)
                return NotFound();

            var post = await _context.Postovi.FirstOrDefaultAsync(p => p.PostId == postId);
            if (post == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(sadrzaj))
            {
                TempData["KomentarGreska"] = "Komentar ne moze biti prazan";
                return RedirectToAction(nameof(Objava), new { id = postId });
            }

            var korisnikId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var komentar = new Komentar
            {
                PostId = postId,
                Sadrzaj = sadrzaj.Trim(),
                DatumPostavljanja = DateTime.UtcNow,
                KorisnikId = korisnikId
            };

            _context.Komentari.Add(komentar);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Objava), new { id = postId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OdgovoriNaKomentar(int postId, int roditeljskiKomentarId, string sadrzaj)
        {
            if (postId <= 0 || roditeljskiKomentarId <= 0)
                return NotFound();

            var parent = await _context.Komentari.FirstOrDefaultAsync(k => k.KomentarId == roditeljskiKomentarId);
            if (parent == null || parent.PostId != postId)
                return NotFound();

            if (parent.RoditeljskiKomentarId != null)
            {
                TempData["KomentarGreska"] = "jedan nivo odgovora!!";
                return RedirectToAction(nameof(Objava), new { id = postId });
            }

            if (string.IsNullOrWhiteSpace(sadrzaj))
            {
                TempData["KomentarGreska"] = "Komentar ne moye biti prazan";
                return RedirectToAction(nameof(Objava), new { id = postId });
            }

            var korisnikId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var komentar = new Komentar
            {
                PostId = postId,
                RoditeljskiKomentarId = roditeljskiKomentarId,
                Sadrzaj = sadrzaj.Trim(),
                DatumPostavljanja = DateTime.UtcNow,
                KorisnikId = korisnikId
            };

            _context.Komentari.Add(komentar);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Objava), new { id = postId });
        }
    }
}
