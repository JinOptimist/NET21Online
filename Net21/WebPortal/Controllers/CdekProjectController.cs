using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models.Cdek;

namespace WebPortal.Controllers;
public class CdekProjectController : Controller
{
    private readonly ICallRequestRepository _repository;

    public CdekProjectController(ICallRequestRepository repository)
    {
        _repository = repository;
    }
    
    // --- Список всех заявок ---
    public IActionResult Index()
    {
        var requests = _repository.GetAll();
        return View(requests);
    }

    // --- Страница с формой заявки ---
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    // --- Обработка отправки формы ---
    [HttpPost]
    public IActionResult Add(CallRequestViewModel request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        _repository.Add(request);
        
        TempData["Message"] = "Заявка отправлена! Менеджер отдела продаж свяжется с Вами в течение 15 минут.";
        
        return RedirectToAction("Index", "CdekProject");
    }

    // --- Удаление заявки ---
    public IActionResult Remove(int id)
    {
        _repository.Remove(id);
        return RedirectToAction("Index");
    }
    
    
    public IActionResult CallRequest()
    {
        return View();
    }
}