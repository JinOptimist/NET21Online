using WebPortal.Models.marketplace.BaseViewModel;

public class CatalogViewModel
{
    public List<ProductViewModel> Products { get; set; } = new();
    public bool CanAdd { get; set; }
    public bool CanEdit { get; set; }
    public bool CanDelete { get; set; }
    public bool CanExport { get; set; }
}