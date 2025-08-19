using WebPortal.DbStuff.Models.Marketplace.BaseItem;

namespace WebPortal.Models.Marketplace
{
    public class CatalogViewModel
    {
        public List<ProductViewModel> Products { get; set; } = new();
    }
}