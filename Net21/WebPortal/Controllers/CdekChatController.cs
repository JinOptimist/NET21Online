using Microsoft.AspNetCore.Mvc;

namespace WebPortal.Controllers;

public class CdekChatController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}