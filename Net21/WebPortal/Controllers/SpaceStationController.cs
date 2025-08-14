using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff;
using WebPortal.Models;
using WebPortal.DbStuff.Models;

namespace WebPortal.Controllers
{
    public class SpaceStationController : Controller
    {
        private WebPortalContext _portalContext;
        public SpaceStationController(WebPortalContext portalContext)
        {
            _portalContext = portalContext;
        }

        public IActionResult Index()
        {
            var SpaceNews = _portalContext
                .SpaceNews
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
            var spaceNewsToRemove = _portalContext.SpaceNews.First(x => x.Id == Id);
            _portalContext.SpaceNews.Remove(spaceNewsToRemove);
            _portalContext.SaveChanges();

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
            _portalContext.SpaceNews.Add(SpaceNewsDb);
            _portalContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}