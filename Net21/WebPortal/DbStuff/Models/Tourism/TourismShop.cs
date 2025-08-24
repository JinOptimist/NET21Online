namespace WebPortal.DbStuff.Models.Tourism
{
    public class TourismShop : BaseModel
    {
        public string TourName { get; set; }
        public string TourImg { get; set; }
        public string? NewAuthor { get;set; }
        public virtual User? AuthorName { get; set; } 
        public virtual List<Tourism> ToursCreatedBasedOnTitle { get; set; } = new List<Tourism>();
    }
}
