namespace WebPortal.Models.SpaceStation
{
    public class AuthorStatisticsViewModel
    {
        public string AuthorName { get; set; }
        public int PublicationCount { get; set; }
        public DateTime LastPublicationDate { get; set; }
        public string LastPublicationTitle { get; set; }
        public int AuthorId { get; set; }
    }
}