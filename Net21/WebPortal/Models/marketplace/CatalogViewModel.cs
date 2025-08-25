using WebPortal.Models.marketplace.BaseViewModel;

namespace WebPortal.Models.Marketplace
{
    public class CatalogViewModel
    {
        public List<ProductViewModel> Products { get; set; } = new();
    }
}