using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebPortal.Models.Cdek;

public class AdminCallRequestIndexViewModel
{
    public List<AdminCallRequestItemViewModel> CallRequests { get; set; }
    public string SelectedStatus { get; set; }
    public string SearchTerm { get; set; }
    public IEnumerable<SelectListItem> StatusOptions { get; set; }
    public List<string> UploadFiles { get; set; } = new();
    
    // Статистика
    public int Всего { get; set; }
    public int Новая { get; set; }
    public int Обработана { get; set; }
    public int ПустойСтатус { get; set; }
}