using Microsoft.AspNetCore.Mvc;
using WebPortal.Models.UnderTheBridge;


namespace WebPortal.Controllers
{
    public class UnderTheBridgeController: Controller
    {
        // Reminder: delete this shit as soon, as you can
        private static List<GuitarViewModel> Guitars = new List<GuitarViewModel>()
        {
            new GuitarViewModel("Cort X100", 4, 201, 200, AccessStatus.InStock, "/images/UnderTheBridge/Guitars/cort-x100-opbk-card.webp"),
            new GuitarViewModel("Ibanez GRG121", 1, 1, 1, AccessStatus.No, "/images/UnderTheBridge/Guitars/ibanez-grg121-card.webp"),
            new GuitarViewModel("Les Paul", 5, 5000, 50000, AccessStatus.InShop, "/images/UnderTheBridge/Guitars/les-paul.webp"),
        };
        
        public IActionResult Index()
        {
            return RedirectToAction("Catalog", "UnderTheBridge");
        }

        public IActionResult Catalog()
        {

            return View(Guitars);
        }

        [HttpGet]
        public IActionResult AddGuitar()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AddGuitar(GuitarViewModel guitar)
        {
            Guitars.Add(guitar);
            return RedirectToAction("Catalog");
        }

        public IActionResult Detail()
        {
            return View();
        }
    }
}