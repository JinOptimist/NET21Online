using Microsoft.AspNetCore.Mvc;
namespace WebPortal.Controllers;
public class CdekProjectController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}