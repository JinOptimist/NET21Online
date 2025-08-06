using Microsoft.AspNetCore.Mvc;


namespace WebPortal.Controllers
{
    public class UnderTheBridgeController: Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Catalog", "UnderTheBridge");
        }

        public IActionResult Catalog()
        {
            var l = new List<string>()
            {
                "cort-x100-opbk-card.webp",
                "ibanez-grg121-card.webp",
                "les-paul.webp",
                "cort-x100-opbk-card.webp",
                "ibanez-grg121-card.webp",
                "les-paul.webp",
                "cort-x100-opbk-card.webp",
                "ibanez-grg121-card.webp",
                "les-paul.webp",
                "cort-x100-opbk-card.webp",
                "ibanez-grg121-card.webp",
                "les-paul.webp",
            };
            
            return View(l);
        }

        public IActionResult Detail()
        {
            return View();
        }
    }
}