using UnderTheBridge.Data.Models;
using static WebPortal.DbStuff.Models.GuitarEntity;

namespace WebPortal.Models.UnderTheBridge
{
    public class GuitarViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public AccessStatus Status { get; set; }
        public int ReviewAmount => Comments.Count;
        public decimal Mark => Math.Round(Comments.Sum(c => c.Mark) / ReviewAmount, 1);
        public List<CommentViewModel> Comments { get; set; }
    }
}
