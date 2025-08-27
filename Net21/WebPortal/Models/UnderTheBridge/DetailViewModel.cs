using WebPortal.DbStuff.Models;

namespace WebPortal.Models.UnderTheBridge
{
    public class DetailViewModel
    {
        public GuitarEntity Guitar {  get; set; }
        public bool IsAuthenticated { get; set; }
        public CommentEntity Comment { get; set; }
    }
}
