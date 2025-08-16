using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff;
using WebPortal.Models.UnderTheBridge;


namespace WebPortal.Controllers
{
    public class UnderTheBridgeController: Controller
    {
        private readonly WebPortalContext db;

        public UnderTheBridgeController(WebPortalContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Catalog", "UnderTheBridge");
        }

        [HttpGet]
        public IActionResult Catalog()
        {
            var view = new CatalogViewModel();

            view.Guitars = db.Guitars.ToList();

            return View(view);
        }

        [HttpGet]
        public IActionResult AddGuitar()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AddGuitar(AddGuitarViewModel view)
        {
            var guitar = view.Guitar;

            db.Guitars.Add(guitar);

            db.SaveChanges();

            return RedirectToAction("Catalog");
        }

        [HttpGet]
        public IActionResult DelGuitar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DelGuitar(DelGuitarViewModel view)
        {
            var guitarId = view.Id;
            var guitar = db.Guitars.Find(guitarId);
            if (guitar == null) return Redirect("DelGuitar");

            db.Guitars.Remove(guitar);

            db.SaveChanges();

            return RedirectToAction("Catalog");
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var view = new DetailViewModel();

            var guitar = db.Guitars.Find(id);

            if (guitar == null) return Redirect("Catalog");
            view.Guitar = guitar;

            return View(view);
        }
    }
}