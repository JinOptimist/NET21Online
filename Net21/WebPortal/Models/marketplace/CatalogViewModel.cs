using WebPortal.DbStuff.Models.Marketplace.BaseItem;

namespace WebPortal.Models.Marketplace
{
    public class CatalogViewModel
    {
        public List<Product> Products { get; set; } = new();
    }
}