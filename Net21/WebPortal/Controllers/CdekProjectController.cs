using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models.Cdek;

namespace WebPortal.Controllers;
public class CdekProjectController : Controller
{
    private readonly ICallRequestRepository _callRequestRepositoryre;

    public CdekProjectController(ICallRequestRepository repository)
    {
        _callRequestRepositoryre = repository;
    }
    
    /// <summary>
    /// Список всех заявок
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        var requests = _callRequestRepositoryre.GetAll();
        return View(requests);
    }

    /// <summary>
    /// Страница с формой заявки
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    /// <summary>
    /// Обработка отправки формы
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Add(CallRequestViewModel request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        _callRequestRepositoryre.Add(request);
        
        ViewBag.Message = "Заявка отправлена! Менеджер свяжется с Вами в течение 15 минут.";
        
        return View(new CallRequestViewModel());    
    }

    /// <summary>
    /// Удаление заявки
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IActionResult Remove(int id)
    {
        _callRequestRepositoryre.Remove(id);
        return RedirectToAction("Index");
    }
}