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
                .FirstOrDefaultAsync(p => p.PostId == id);

            if (objava == null)
                return NotFound();

            return View(objava);
        }
    }
}
