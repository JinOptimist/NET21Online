using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff;
using WebPortal.Models;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories;
using WebPortal.DbStuff.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortal.Models.Girls;

namespace WebPortal.Controllers
{
    public class SpaceStationController : Controller
    {
        private ISpaceStationRepository _spaceStationRepository;
        private IUserRepositrory _userRepositrory;

        public SpaceStationController(ISpaceStationRepository spaceStationRepository, IUserRepositrory userRepositrory)
        {
            _spaceStationRepository = spaceStationRepository;
            _userRepositrory = userRepositrory;
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
                    Content = dbSpaceNews.Content,
                    AuthorName = dbSpaceNews.Author?.UserName ?? "John Doe"
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
            var users = _userRepositrory.GetAll();
            var viewModel = new SpaceNewsViewModel();
            viewModel.AllUsers = users
                .Select(x => new SelectListItem
                {
                    Text = x.UserName,
                    Value = x.Id.ToString()
                })
                .ToList();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult News(SpaceNewsViewModel spaceNewsViewModel)

        {
            var authorId = spaceNewsViewModel.AuthorId;
            var author = _userRepositrory.GetFirstById(authorId);

            var SpaceNewsDb = new SpaceNews()
            {
                Title = spaceNewsViewModel.Title,
                DateAdded = spaceNewsViewModel.DateAdded,
                Url = spaceNewsViewModel.ImageUrl,
                Content = spaceNewsViewModel.Content,
                Author = author
            };
            _spaceStationRepository.Add(SpaceNewsDb);

            return RedirectToAction("Index");
        }
    }
}