using Microsoft.AspNetCore.Mvc;
namespace WebPortal.Controllers;
public class SdekProjectController : Controller
{
    private static List<string> SdekList = new List<string>();
    
    // GET
    public IActionResult Index()
    {
        return View(SdekList);
    }
    
    [HttpGet]
    public IActionResult ChatBot()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult ChatBot(string name, string question, string number)
    {
        SdekList.Add(question);
        return RedirectToAction("ChatBot");
    }
}