namespace RvasApp.Models
{
    public class KomentarVote//termin 11 zadatak sa vezbi
    {
        public int KomentarVoteId { get; set; }

        public int KomentarId { get; set; }
        public Komentar? Komentar { get; set; }

        public string KorisnikId { get; set; } = null!;
        public Korisnik? Korisnik { get; set; }

        public bool IsUpvote { get; set; }
    }
}
