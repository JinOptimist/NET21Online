namespace WebPortal.DbStuff.Models.Tourism
{
    public class TourPreview : BaseModel
    {
        public string TourName { get; set; }
        public string TourImgUrl { get; set; }
        public int DaysToPrepareTour { get; set; }
        public int TourRating { get; set; }

    }
}
