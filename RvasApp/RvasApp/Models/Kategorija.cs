namespace RvasApp.Models
{
    public class Kategorija
    {
        public int KategorijaId { get; set; }
        public string Naziv { get; set; }

        public List<Post> Postovi { get; set; } = new();
    }
}
