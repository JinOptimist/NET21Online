using WebPortal.Models.UnderTheBridge;

namespace WebPortal.DbStuff.Models
{
    public class GuitarEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public decimal Mark { get; set; } = decimal.Zero;
        public int ReviewAmount { get; set; } = 0;

        public AccessStatus Status { get; set; } = AccessStatus.No;

        public enum AccessStatus
        {
            InShop = 0,
            InStock = 1,
            No = 3
        }
    }
}
