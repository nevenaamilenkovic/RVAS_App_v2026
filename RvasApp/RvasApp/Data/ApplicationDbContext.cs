using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RvasApp.Models;

namespace RvasApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<Korisnik>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Kurs> Kursevi {  get; set; }
        public DbSet<Prijava> Prijave {  get; set; }
        public DbSet<Lekcija> Lekcije {  get; set; }
        public DbSet<LekcijaMaterijali> Materijali {  get; set; }
        public DbSet<Post> Postovi { get; set; }
        public DbSet<Komentar> Komentari { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //pri uklanjanju kursa -> ispisuju se i svi polaznici sa tog kursa
            //postavljanje pravila stranog kljuca! nakon ovoga obavezno add-migration i update-database

            //builder.Entity<Prijava>()
            //    .HasOne(p => p.Kurs)
            //    .WithMany(k => k.Prijave)
            //    .HasForeignKey(p => p.KursId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //na postu je podrazumevan delete behavior cascade, dakle uklanjanjem posta, brisu se i svi komentari vezani za isti
            //prilikom brisanja korisnickog naloga komentari tog korisnika ostaju!
            builder.Entity<Komentar>()
                .HasOne(k => k.Korisnik)
                .WithMany()
                .HasForeignKey(k => k.KorisnikId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
