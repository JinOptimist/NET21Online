using WebPortal.DbStuff.Models;

namespace WebPortal.Models.UnderTheBridge
{
    public class CommentViewModel
    {
        public string Message { get; set; }
        public decimal Mark { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Author { get; set; }
    }
}
