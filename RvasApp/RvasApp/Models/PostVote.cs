namespace RvasApp.Models
{
    public class PostVote
    {
        //bolje nego lajk dislajk haha
        //ali logika je ista
        public int PostVoteId { get; set; }

        //glas se odnosi na post
        public int PostId { get; set; }
        public Post? Post { get; set; }

        //ulogovani korisnik moze da glasa
        public string KorisnikId { get; set; } = null!;
        public Korisnik? Korisnik { get; set; }
        //true ako je up, false ako je down
        public bool IsUpvote { get; set; }
    }
}
