namespace RvasApp.Models.ViewModels
{
    public class PostVoteViewModel
    {
        public int PostId { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public bool? UserVote { get; set; }
    }
}
