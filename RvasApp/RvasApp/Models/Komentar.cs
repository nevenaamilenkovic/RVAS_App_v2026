namespace RvasApp.Models
{
    public class Komentar
    {
        //jedinstveni identifikator komentara
        public int KomentarId { get; set; }
        //tekst komentara, sadrzaj
        public string Sadrzaj { get; set; }
        //kada je ostavljen komentar
        public DateTime DatumPostavljanja { get; set; }
        //ko je ostavio komentar
        public string KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        //komentar se odnosi na objavu:
        public int PostId { get; set; }
        public Post Post { get; set; }

        //na koji komentar se odgovara (komentarom)
        public int? RoditeljskiKomentarId { get; set; }
        public Komentar? RoditeljskiKomentar { get; set; }
        //jedan komentar moze imati vise odgovora
        public List<Komentar> Odgovori { get; set; } = new();

    }
}
