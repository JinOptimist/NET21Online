namespace WebPortal.Models
{
    public class SpaceNewsViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}