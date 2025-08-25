namespace WebPortal.DbStuff.Models
{
    public class GuitarEntity: BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public virtual List<CommentEntity> Comments { get; set; } = new();

        public AccessStatus Status { get; set; } = AccessStatus.No;

        public enum AccessStatus
        {
            InShop = 0,
            InStock = 1,
            No = 3
        }
    }

    public static class RatingExtention
    {
        public static int GetReviewsAmount(this List<CommentEntity> comments)
        {
            var reviews = comments.Count;

            return reviews;
        }

        public static decimal GetMark(this List<CommentEntity> comments)
        {
            var mark = comments.Select(c => c.Mark).Sum() / GetReviewsAmount(comments);
            mark = Math.Round(mark, 1);

            return mark;
        }
    }
}
