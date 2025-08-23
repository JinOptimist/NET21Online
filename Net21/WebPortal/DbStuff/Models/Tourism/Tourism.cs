namespace WebPortal.DbStuff.Models.Tourism
{
    public class Tourism : BaseModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public int Days { get; set; }
        public int TitleRating { get; set; }

    }
}
