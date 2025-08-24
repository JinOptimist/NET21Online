using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.Models.Cdek;

namespace WebPortal.Controllers;

public class AdminCallRequestController : Controller
{
    private readonly IAdminCallRequestRepository _repository;

    public AdminCallRequestController(IAdminCallRequestRepository repository)
    {
        _repository = repository;
    }

        // --- Список всех заявок с фильтрацией ---
    public IActionResult Index(string search = "", string statusFilter = "")
    {
        var requests = _repository.GetAll()
            .Select(r => new AdminCallRequestViewModel
            {
                Id = r.Id,
                Name = r.Name,
                PhoneNumber = r.PhoneNumber,
                Question = r.Question,
                Status = r.Status,
                CreatedAt = r.CreatedAt
            });

        if (!string.IsNullOrEmpty(search))
        {
            requests = requests.Where(r =>
                r.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                r.PhoneNumber.Contains(search));
        }

        if (!string.IsNullOrEmpty(statusFilter))
        {
            requests = requests.Where(r => r.Status == statusFilter);
        }

        return View(requests.OrderByDescending(r => r.CreatedAt).ToList());
    }

    public IActionResult Remove(int id)
    {
        _repository.Remove(id);
        return RedirectToAction("Index");
    }

    public IActionResult ChangeStatus(int id)
    {
        var request = _repository.GetById(id);
        if (request != null)
        {
            request.Status = request.Status == "Новая" ? "Обработана" : "Новая";
            _repository.Update(request);
        }

        return RedirectToAction("Index");
    }
}
