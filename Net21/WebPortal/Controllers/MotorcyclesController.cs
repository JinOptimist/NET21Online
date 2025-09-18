using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models.Motorcycles;
using WebPortal.Models;
using WebPortal.Models.Motorcycles;
using WebPortal.DbStuff.Repositories;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebPortal.Controllers
{
    public class MotorcyclesController : Controller
    {
        private IMotorcycleRepository _motorcycleRepository;
        private AuthService _authService;

        public MotorcyclesController(IMotorcycleRepository motorcycleRepository, AuthService authService)
        {
            _motorcycleRepository = motorcycleRepository;
            _authService = authService;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeMotorcycleViewModels();
            if (_authService.IsAuthenticated())
            {
                var userId = _authService.GetId();
                var userName = _authService.GetUser().UserName;
                viewModel.UserId = userId;
                viewModel.UserName = userName;
            }
            else 
            {
                viewModel.UserId = 0;
                viewModel.UserName = "Guess";

            }

            var motorcycles = _motorcycleRepository
                    .GetNewMotorcycle()
                    .Select(dbMotorcycles => new MotorcyclesViewModel
                    {
                        Name = dbMotorcycles.Model,
                        Src = dbMotorcycles.ImageSrc,
                        Description = dbMotorcycles.Description,
                        Id = dbMotorcycles.Id,
                    })
                    .ToList();
            viewModel.Motorcycles = motorcycles;    
            return View(viewModel);
        }
        public IActionResult Remove(int Id)
        {
            _motorcycleRepository.Remove(Id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddBike() 
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddBike(MotorcyclesViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var authorName = _authService.GetUser().UserName;
            var authorId = _authService.GetUser().Id;
            var dbMotorcycle = new Motorcycle()
            {
                ImageSrc = model.Src,
                Description = model.Description,
                Model = model.Name,
                MotorcycleType = model.Name,
                AuthorName = authorName,
                AuthorId = authorId
            };
            
            _motorcycleRepository.Add(dbMotorcycle);
            return RedirectToAction("Index");
        }
    }
}
