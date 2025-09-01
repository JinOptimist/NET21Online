using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.Models.Cdek;
using WebPortal.Services;
using WebPortal.Services.Permissions;

namespace WebPortal.Controllers;

public class AdminCdekProjectController : Controller
{
    private readonly IAdminCallRequestRepository _adminCallRequestRepository;
    private IUserRepositrory _userRepositrory;
    private AuthService _authService;
    private AdminCallRequestPermission _adminCallRequestPermission;

    public AdminCdekProjectController(
        IAdminCallRequestRepository adminCallRequestRepository, 
        IUserRepositrory userRepositrory, 
        AuthService authService,
        AdminCallRequestPermission adminCallRequestPermission)
    {
        _adminCallRequestRepository = adminCallRequestRepository;
        _userRepositrory = userRepositrory;
        _authService = authService;
        _adminCallRequestPermission = adminCallRequestPermission;
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
    
    /// <summary>
    /// Удаление заявки
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    public IActionResult Remove(int id)
    {
        _adminCallRequestRepository.Remove(id);
        return RedirectToAction("Index");
    }
    
    /// <summary>
    ///  Изменение статуса заявки
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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