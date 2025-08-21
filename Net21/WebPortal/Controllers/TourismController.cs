using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models;
using WebPortal.Models.Tourism;

namespace WebPortal.Controllers
{
    public class TourismController : Controller
    {
        public ITourismRepository _tourismRepository;

        public TourismController(ITourismRepository tourismRepository)
        {
            _tourismRepository = tourismRepository;
        }

        public IActionResult Index()
        {
            var titleNames = _tourismRepository
                .GetPopularListTitles()
                .Select(dbData=> new TourismViewModel
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
            var tourismBd = new Tourism()
            {
                Title = viewModel.Title,
                Days = (int)viewModel.Days,
                Url = viewModel.URL,
                TitleRating = 0
            };
            _tourismRepository.Add(tourismBd);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            _tourismRepository.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
