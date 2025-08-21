using Microsoft.AspNetCore.Mvc;
namespace WebPortal.Controllers;
public class SdekProjectController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
