using WebPortal.DbStuff.Models.Marketplace.BaseItem;

namespace WebPortal.DbStuff.Models.Marketplace
{
    public class ProductImages
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public bool IsMain { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
