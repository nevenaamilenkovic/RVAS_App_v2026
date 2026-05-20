namespace RvasApp.Models.ViewModels
{
    public class KomentarVoteViewModel
    {
        public int KomentarId { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public bool? UserVote { get; set; }
    }
}
