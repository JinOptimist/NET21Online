using WebPortal.DbStuff.Models.Marketplace.BaseItem;

namespace WebPortal.Models.Marketplace
{
    public class CatalogViewModel : ProductBaseViewModel
    {
        public List<ProductViewModel> Products { get; set; } = new();
    }
}