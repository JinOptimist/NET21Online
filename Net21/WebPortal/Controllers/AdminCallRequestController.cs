using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.Models.Cdek;

namespace WebPortal.Controllers;

public class AdminCallRequestController : Controller
{
    private readonly IAdminCallRequestRepository _adminCallRequestRepository;

    public AdminCallRequestController(IAdminCallRequestRepository repository)
    {
        _adminCallRequestRepository = repository;
    }

        /// <summary>
        /// Список всех заявок, отфильтрован в репозитории
        /// </summary>
        /// <param name="search"></param>
        /// <param name="statusFilter"></param>
        /// <returns></returns>
        public IActionResult Index(string search = "", string statusFilter = "")
    {
        var requests = _adminCallRequestRepository.GetFilteredRequests(search, statusFilter);
        return View(requests);
    }

    public IActionResult Remove(int id)
    {
        _adminCallRequestRepository.Remove(id);
        return RedirectToAction("Index");
    }

    public IActionResult ChangeStatus(int id)
    {
        var request = _adminCallRequestRepository.GetById(id);
        if (request != null)
        {
            request.Status = request.Status == "Новая" ? "Обработана" : "Новая";
            _adminCallRequestRepository.Update(request);
        }

        return RedirectToAction("Index");
    }
}
