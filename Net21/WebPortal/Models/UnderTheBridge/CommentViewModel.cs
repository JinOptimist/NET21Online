namespace WebPortal.Models.UnderTheBridge
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string AuthorName { get; set; }
        public decimal Mark { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool CanDelete { get; set; }
    }
}
