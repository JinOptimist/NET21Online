using Microsoft.AspNetCore.Mvc;
using System.IO.Pipes;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models;
using WebPortal.Models;

namespace WebPortal.Controllers
{
    public class GirlController : Controller
    {
        private WebPortalContext _portalContext;

        public GirlController(WebPortalContext portalContext)
        {
            _portalContext = portalContext;
        }

        public IActionResult Index()
        {
            var girls = _portalContext
                .Girls
                .OrderBy(x => x.Size)
                .Take(100)
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
            var girlForRemove = _portalContext.Girls.First(x => x.Id == Id);
            _portalContext.Girls.Remove(girlForRemove);
            _portalContext.SaveChanges();

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
            _portalContext.Girls.Add(girlDb);
            _portalContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
