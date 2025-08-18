using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Repositories;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models.UnderTheBridge;


namespace WebPortal.Controllers
{
    public class UnderTheBridgeController: Controller
    {
        private readonly IGuitarRepository Guitars;

        public UnderTheBridgeController(IGuitarRepository guitarRepository)
        {
            Guitars = guitarRepository;
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

            view.Guitars = Guitars.GetAll();

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

            Guitars.Add(guitar);

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

            var guitar = Guitars.GetById(guitarId);

            if (guitar == null)
            {
                throw new Exception("No such guitar");
            }

            Guitars.Remove(guitar);

            return RedirectToAction("Catalog");
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var view = new DetailViewModel();

            var guitar = Guitars.GetById(id);

            if (guitar == null)
            {
                throw new Exception("No such guitar");
            }

            view.Guitar = guitar;

            return View(view);
        }
    }
}