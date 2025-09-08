using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortal.Controllers.CustomAuthorizeAttributes;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;
using WebPortal.Enum;
using WebPortal.Models.Cdek;
using WebPortal.Services;
using WebPortal.Services.Permissions;

namespace WebPortal.Controllers;

[Authorize]
public class AdminCdekProjectController : Controller
{
    private readonly IAdminCallRequestRepository _adminCallRequestRepository;
    private IUserRepositrory _userRepositrory;
    private AuthService _authService;
    private IAdminCallRequestPermission _adminCallRequestPermission;

    public AdminCdekProjectController(
        IAdminCallRequestRepository adminCallRequestRepository, 
        IUserRepositrory userRepositrory, 
        AuthService authService,
        IAdminCallRequestPermission adminCallRequestPermission)
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

        var viewModel = new AdminCallRequestIndexViewModel
        {
            CallRequests = requests
                .Select(r => new AdminCallRequestItemViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    PhoneNumber = r.PhoneNumber,
                    Question = r.Question,
                    Status = r.Status,
                    CreatedAt = r.CreatedAt,
                    CanDelete = _adminCallRequestPermission.CanDelete(r),
                }).ToList(),

            SearchTerm = search,
            SelectedStatus = statusFilter,
            StatusOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Все статусы" },
                new SelectListItem { Value = "Новая", Text = "Новая" },
                new SelectListItem { Value = "Обработана", Text = "Обработана" }
            }
        };
        
        return View(viewModel);
    }
    
    /// <summary>
    /// Удаление заявки
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Role(Role.Admin)]
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