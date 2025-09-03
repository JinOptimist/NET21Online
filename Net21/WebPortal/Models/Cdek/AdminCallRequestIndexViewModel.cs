using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebPortal.Models.Cdek;

public class AdminCallRequestIndexViewModel
{
    public List<AdminCallRequestItemViewModel> Requests { get; set; }
    public string SelectedStatus { get; set; }
    public string SearchTerm { get; set; }
    public IEnumerable<SelectListItem> StatusOptions { get; set; }
}