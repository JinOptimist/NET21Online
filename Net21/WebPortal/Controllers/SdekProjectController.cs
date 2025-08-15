using Microsoft.AspNetCore.Mvc;
using WebPortal.Models;

namespace WebPortal.Controllers;
public class SdekProjectController : Controller
{
    

    
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult CallRequest()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult CallRequest(CdekViewModel cdekViewModel)
    {
        return RedirectToAction("Index");
    }
}