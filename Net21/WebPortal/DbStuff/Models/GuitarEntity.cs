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
}
