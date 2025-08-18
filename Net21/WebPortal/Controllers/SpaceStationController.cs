using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff;
using WebPortal.Models;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.Controllers
{
    public class SpaceStationController : Controller
    {
        private ISpaceStationRepository _spaceStationRepository;

        public SpaceStationController(ISpaceStationRepository spaceStationRepository)
        {
            _spaceStationRepository = spaceStationRepository;
        }

        public IActionResult Index()
        {
            var SpaceNews = _spaceStationRepository
                .FirstNews()
                .Select(dbSpaceNews =>
                new SpaceNewsViewModel
                {   
                    Id = dbSpaceNews.Id,
                    Title = dbSpaceNews.Title,
                    DateAdded = dbSpaceNews.DateAdded,
                    ImageUrl = dbSpaceNews.Url,
                    Content = dbSpaceNews.Content
                })
                .ToList();

            return View(SpaceNews);
        }

        public IActionResult remove(int Id) 
        {
            _spaceStationRepository.Remove(Id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult News()
        {
            return View();
        }

        [HttpPost]
        public IActionResult News(SpaceNewsViewModel spaceNewsViewModel)

        {
            var SpaceNewsDb = new SpaceNews()
            {
                Title = spaceNewsViewModel.Title,
                DateAdded = spaceNewsViewModel.DateAdded,
                Url = spaceNewsViewModel.ImageUrl,
                Content = spaceNewsViewModel.Content
            };
            _spaceStationRepository.Add(SpaceNewsDb);

            return RedirectToAction("Index");
        }
    }
}