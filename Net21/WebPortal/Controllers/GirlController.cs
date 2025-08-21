using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models.Girls;

namespace WebPortal.Controllers
{
    public class GirlController : Controller
    {
        private IGirlRepository _girlRepository;
        private IUserRepositrory _userRepositrory;

        public GirlController(IGirlRepository girlRepository, IUserRepositrory userRepositrory)
        {
            _girlRepository = girlRepository;
            _userRepositrory = userRepositrory;
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
                        Rating = dbGirl.Size * 2 + 20 - dbGirl.Age,
                        AuthorName = dbGirl.Author?.UserName ?? "NoAuthor"
                    })
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
            var users = _userRepositrory.GetAll();
            var viewModel = new GirlCreationViewModel();
            viewModel.AllUsers = users
                .Select(x => new SelectListItem
                {
                    Text = x.UserName,
                    Value = x.Id.ToString()
                })
                .ToList();

            return View(viewModel);
        }

        // /Girl/Add  <== HTTP POST
        [HttpPost]
        public IActionResult Add(GirlCreationViewModel girlViewModel)
        {
            if (!ModelState.IsValid)
            {
                var users = _userRepositrory.GetAll();
                girlViewModel.AllUsers = users
                    .Select(x => new SelectListItem
                    {
                        Text = x.UserName,
                        Value = x.Id.ToString()
                    })
                    .ToList();
                return View(girlViewModel);
            }

            var authorId = girlViewModel.AuthorId;
            var author = _userRepositrory.GetFirstById(authorId);

            // Remember new Girl
            var girlDb = new Girl()
            {
                Age = 18,
                Name = girlViewModel.Name,
                Size = 5,
                Url = girlViewModel.Src,
                Author = author
            };
            _girlRepository.Add(girlDb);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Link()
        {
            var linkGirlView = new LinkGirlViewModel();
            linkGirlView.AllGirls = _girlRepository
                .GetAllWithAuthor()
                .OrderBy(x => x.Author?.Id ?? -1)
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
                .ToList();

            linkGirlView.AllUsers = _userRepositrory
                .GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.UserName,
                    Value = x.Id.ToString()
                })
                .ToList();

            return View(linkGirlView);
        }

        [HttpPost]
        public IActionResult Link(LinkGirlViewModel linkGirlView)
        {
            var user = _userRepositrory.GetFirstById(linkGirlView.AuthorId);
            var girl = _girlRepository.GetFirstById(linkGirlView.GirlId);

            girl.Author = user;
            _girlRepository.Update(girl);

            return RedirectToAction("Index");
        }
    }
}
