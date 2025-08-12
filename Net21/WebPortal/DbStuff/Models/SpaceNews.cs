namespace WebPortal.DbStuff.Models
{
    public class SpaceNews:BaseModel
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
