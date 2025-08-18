using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models;
using WebPortal.Models;
using WebPortal.Models.Tourism;

namespace WebPortal.Controllers
{
    public class TourismController : Controller
    {
        private WebPortalContext _portalcontext;

        public TourismController(WebPortalContext portalcontext)
        {
            _portalcontext = portalcontext;
        }

        public IActionResult Index()
        {
            var titleNames = _portalcontext.
                Tourisms.
                Select(dbData=> new TourismViewModel
                {
                    Title = dbData.Title,
                    URL = dbData.Url,
                    Days = dbData.Days,
                    Id = dbData.Id
                }).
                ToList();
        
            return View(titleNames);
        }

        [HttpGet]
        public IActionResult AddContent()
        {
         
            return View();
        }


        [HttpPost]
        public IActionResult AddContent(TourismViewModel viewModel)
        {
            _portalcontext.Tourisms.Add(new Tourism()
            {
                Title = viewModel.Title,
                Days = (int)viewModel.Days,
                Url = viewModel.URL,
            });
            _portalcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int Id)
        {
            var titlefordeleting = _portalcontext.Tourisms.First(x => x.Id == Id);
            _portalcontext.Tourisms.Remove(titlefordeleting);
            _portalcontext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
