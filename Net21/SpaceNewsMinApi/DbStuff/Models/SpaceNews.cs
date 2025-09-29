namespace SpaceNewsMinApi.DbStuff.Models
{
    public class SpaceNews : BaseModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
    }
}
