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
        public DbSet<Kategorija> Kategorije { get; set; }


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
            builder.Entity<Komentar>()
                .HasOne(k => k.Korisnik)
                .WithMany()
                .HasForeignKey(k => k.KorisnikId)
                .OnDelete(DeleteBehavior.NoAction);//ovde je promenjen delete behavior na none, sto znaci
            //da ce program "puci", samim tim ne bi bilo lose da probate da rukujete rucno ovakvim izuzetkom

            //self reference za komentare!!
            builder.Entity<Komentar>()
                .HasOne(k => k.RoditeljskiKomentar)
                .WithMany(k => k.Odgovori)
                .HasForeignKey(k => k.RoditeljskiKomentarId)
                .OnDelete(DeleteBehavior.NoAction);//mada bi bolje bilo cascade
            //ovde je promenjen delete behavior na none, sto znaci
            //da ce program "puci", samim tim ne bi bilo lose da probate da rukujete rucno ovakvim izuzetkom

            //predefinisane kategorije
            //dodati migraciju nakon ovoga! pa tek onda update baze, ovo je jos jedan od nacina za pre-seed podataka
            //ovo je najbolji nacin!
            builder.Entity<Kategorija>().HasData(
                new Kategorija { KategorijaId=1,Naziv="Pitanja"},
                new Kategorija { KategorijaId=2,Naziv="Saveti"},
                new Kategorija { KategorijaId=3,Naziv="IT"},
                new Kategorija { KategorijaId=4, Naziv="Novosti"}
            );
        }
    }
}
