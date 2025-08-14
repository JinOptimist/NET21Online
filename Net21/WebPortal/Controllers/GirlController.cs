using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models;

namespace WebPortal.Controllers
{
    public class GirlController : Controller
    {
        private IGirlRepository _girlRepository;

        public GirlController(IGirlRepository girlRepository)
        {
            _girlRepository = girlRepository;
        }

        public IActionResult Index()
        {
            var girls = _girlRepository
                .GetMostPopular()
                .Select(dbGirl => 
                    new GirlViewModel
                    {
                        Id = dbGirl.Id,
                        Name = dbGirl.Name,
                        CreationTime = DateTime.Now,
                        Src = dbGirl.Url,
                        Rating = dbGirl.Size * 2 + 20 - dbGirl.Age
                    } )
                .ToList();

            return View(girls);
        }

        public IActionResult Remove(int Id)
        {
            _girlRepository.Remove(Id);

            return RedirectToAction("Index");
        }

        // /Girl/Add  <== HTTP GET
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // /Girl/Add  <== HTTP POST
        [HttpPost]
        public IActionResult Add(GirlViewModel girlViewModel)
        {
            // Remember new Girl
            var girlDb = new Girl()
            {
                Age = 18,
                Name = girlViewModel.Name,
                Size = 5,
                Url = girlViewModel.Src
            };
            _girlRepository.Add(girlDb);

            return RedirectToAction("Index");
        }
    }
}
