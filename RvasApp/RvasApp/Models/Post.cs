namespace RvasApp.Models
{
    public class Post
    {
        public int PostId { get; set; }

        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime DatumKreiranja { get; set; }

        public string? KorisnikId { get; set; }
        public Korisnik? Korisnik { get; set; }

        //komentari na postu
        public List<Komentar> Komentari { get; set; } = new();

        public int? KategorijaId { get; set; }
        public Kategorija? Kategorija { get; set; }
    }
}
