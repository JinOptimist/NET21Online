using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models.Cdek;

namespace WebPortal.Controllers;
public class CdekProjectController : Controller
{
    private readonly ICallRequestRepository _callRequestRepository;

    public CdekProjectController(ICallRequestRepository repository)
    {
        _callRequestRepository = repository;
    }
    
    /// <summary>
    /// Выводит на экран страницу Index.cshtml
    /// </summary>
    /// <returns></returns>
     public IActionResult Index()
    {
        return View();
    }
     
    /// <summary>
    /// Выводит на экран страницу CallRequest.cshtml
    /// </summary>
    /// <returns></returns>
   [HttpGet]
    public IActionResult CallRequest()
    {
        return View();
    }

    /// <summary>
    /// Обработка отправки формы
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult CallRequest(CallRequestViewModel request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        _callRequestRepository.Add(request);
        
        TempData["Message"] = "Заявка отправлена! Менеджер свяжется с Вами в течение 15 минут.";

        return RedirectToAction("Index",  "CdekProject");    
    }
}