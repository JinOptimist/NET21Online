using WebPortal.Models.Marketplace;

namespace WebPortal.Models.marketplace
{
    public class CatalogViewModel
    {
        public List<ProductBase> Products { get; set; } = new();
        public List<string> Categories { get; set; } = new();
        public string SelectedCategory { get; set; }
        public string SearchQuery { get; set; }
    }
}
