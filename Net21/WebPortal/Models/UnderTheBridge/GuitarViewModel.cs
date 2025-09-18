using WebPortal.DbStuff.Models;

namespace WebPortal.Models.UnderTheBridge
{
    public class GuitarViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public GuitarEntity.AccessStatus Status { get; set; }
        public int ReviewAmount => CommentMarks.Count;
        public decimal Mark => Math.Round(CommentMarks.Sum() / ReviewAmount, 1);
        public List<decimal> CommentMarks { get; set; }
    }
}
