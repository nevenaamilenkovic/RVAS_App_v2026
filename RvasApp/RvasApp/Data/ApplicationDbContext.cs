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
        public DbSet<PostVote> PostGlasovi { get; set; }
        public DbSet<KomentarVote> KomentarGlasovi { get; set; }//termin 11 zadatak sa vezbi


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

            builder.Entity<PostVote>()
                .HasOne(v => v.Post)
                .WithMany(p => p.Glasovi)
                .HasForeignKey(v => v.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PostVote>()
                .HasOne(v => v.Korisnik)
                .WithMany()
                .HasForeignKey(v => v.KorisnikId)
                .OnDelete(DeleteBehavior.NoAction);

            //jedinstveni indeks samo jedan glas/vote moze po korisniku
            builder.Entity<PostVote>()
                .HasIndex(v => new { v.PostId, v.KorisnikId })
                .IsUnique();

            //termin 11 zadatak sa vezbi
            builder.Entity<KomentarVote>()
                .HasOne(v => v.Komentar)
                .WithMany(k => k.Glasovi)
                .HasForeignKey(v => v.KomentarId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<KomentarVote>()
                .HasOne(v => v.Korisnik)
                .WithMany()
                .HasForeignKey(v => v.KorisnikId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<KomentarVote>()
                .HasIndex(v => new { v.KomentarId, v.KorisnikId })
                .IsUnique();

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
