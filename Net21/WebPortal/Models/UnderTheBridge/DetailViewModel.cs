namespace WebPortal.Models.UnderTheBridge
{
    public class DetailViewModel
    {
        public GuitarViewModel Guitar { get; set; }
        public List<CommentViewModel> Comments { get; set; } = new();
        public bool IsAuthenticated { get; set; }
        public CommentCreateViewModel CommentForm { get; set; }
    }
}
