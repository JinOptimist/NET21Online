using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebPortal.Models.Cdek;

public class AdminCallRequestIndexViewModel
{
    public List<AdminCallRequestItemViewModel> CallRequests { get; set; }
    public string SelectedStatus { get; set; }
    public string SearchTerm { get; set; }
    public IEnumerable<SelectListItem> StatusOptions { get; set; }
    public List<AdminCdekFileViewModel> UploadFiles { get; set; } = new();
    public List<AdminCdekStatusViewModel> StatusStats { get; set; }
}