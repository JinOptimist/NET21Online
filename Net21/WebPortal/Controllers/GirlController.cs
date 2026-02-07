using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortal.Controllers.CustomAuthorizeAttributes;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Enum;
using WebPortal.Models.Girls;
using WebPortal.Services;
using WebPortal.Services.Apis;
using WebPortal.Services.Permissions;

namespace WebPortal.Controllers
{
    [Authorize]
    [NoBadWord]
    public class GirlController : Controller
    {
        private IGirlRepository _girlRepository;
        private IUserRepositrory _userRepositrory;
        private IAuthService _authService;
        private IGirlPermission _girlPermission;
        private WaifuApi _waifuApi;

        public GirlController(
            IGirlRepository girlRepository,
            IUserRepositrory userRepositrory,
            IAuthService authService,
            IGirlPermission girlPermission,
            WaifuApi waifuApi)
        {
            _girlRepository = girlRepository;
            _userRepositrory = userRepositrory;
            _authService = authService;
            _girlPermission = girlPermission;
            _waifuApi = waifuApi;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var httpCLient = new HttpClient();
            //var answer = await httpCLient
            //    .GetFromJsonAsync<string[]>("https://localhost:7193/GetUrls");

            var girls = _girlRepository
                .GetMostPopular()
                .Select(dbGirl => new GirlViewModel
                {
                    Id = dbGirl.Id,
                    Name = dbGirl.Name,
                    CreationTime = DateTime.Now,
                    Src = dbGirl.Url,
                    AuthorName = dbGirl.Author?.UserName ?? "NoAuthor",
                    CanDelete = _girlPermission.CanDelete(dbGirl),
                })
                .ToList();

            var viewModel = new IndexViewModel();
            viewModel.GirlsFromDb = girls;

            var girlsFromApi = new List<GirlFromApiViewModel>();
            var tags = await _waifuApi.GetTags(); // 10sec
            var listOfTasks = new List<(Task<string>, string)>();
            foreach (var tag in tags.Take(2))
            {
                var task = _waifuApi.GetWaifu(tag);
                listOfTasks.Add((task, tag));
            }

            Task.WaitAll(listOfTasks.Select(x => x.Item1).ToArray());
            // var taskParentOfAll = Task.WhenAll(listOfTasks);

            foreach (var task in listOfTasks)
            {
                var girlFromApi = new GirlFromApiViewModel
                {
                    Url = task.Item1.Result,
                    Tag = task.Item2
                };
                girlsFromApi.Add(girlFromApi);
            }

            viewModel.GirlFromApi = girlsFromApi;

            return View(viewModel);
        }

        public IActionResult Remove(int Id)
        {
            _girlRepository.Remove(Id);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Table(string? fieldName, SortDirection sortDirection = SortDirection.Ascending)
        {
            var girls = _girlRepository
                .GetAllAfterSort(fieldName, sortDirection)
                .Select(dbGirl => new GirlViewModel
                {
                    Id = dbGirl.Id,
                    Name = dbGirl.Name,
                    CreationTime = DateTime.Now,
                    Src = dbGirl.Url,
                    AuthorName = dbGirl.Author?.UserName ?? "NoAuthor",
                    CanDelete = _girlPermission.CanDelete(dbGirl),
                    // Добавляем все поля из Girl
                    Age = dbGirl.Age,
                    Size = dbGirl.Size,
                    Url = dbGirl.Url,
                    // Добавляем все поля из User (Author)
                    AuthorId = dbGirl.Author?.Id ?? 0,
                    AuthorUserName = dbGirl.Author?.UserName ?? "NoAuthor",
                    AuthorPassword = dbGirl.Author?.Password ?? "",
                    AuthorAvatarUrl = dbGirl.Author?.AvatarUrl ?? "",
                    AuthorMoney = dbGirl.Author?.Money ?? 0,
                    AuthorRole = dbGirl.Author?.Role.ToString() ?? "Unknown",
                    AuthorLanguage = dbGirl.Author?.Language.ToString() ?? "Unknown"
                })
                .ToList();

            var viewModel = new IndexViewModel();
            viewModel.GirlsFromDb = girls;
            return View(viewModel);
        }

        // /Girl/Add  <== HTTP GET
        [HttpGet]
        [Role(Role.GrilModrator, Role.Admin)]
        public IActionResult Add()
        {
            var users = _userRepositrory.GetAll();
            var viewModel = new GirlCreationViewModel();
            viewModel.AllUsers = users
                .Select(x => new SelectListItem { Text = x.UserName, Value = x.Id.ToString() })
                .ToList();

            return View(viewModel);
        }

        // /Girl/Add  <== HTTP POST
        [HttpPost]
        [Role(Role.GrilModrator, Role.Admin)]
        public IActionResult Add(GirlCreationViewModel girlViewModel)
        {
            if (!ModelState.IsValid)
            {
                var users = _userRepositrory.GetAll();
                girlViewModel.AllUsers = users
                    .Select(x => new SelectListItem { Text = x.UserName, Value = x.Id.ToString() })
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
                Author = author,
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
                .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
                .ToList();

            linkGirlView.AllUsers = _userRepositrory
                .GetAll()
                .Select(x => new SelectListItem { Text = x.UserName, Value = x.Id.ToString() })
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

        [Authorize]
        public IActionResult UpdateName(int id, string name)
        {
            var user = _authService.GetUser();

            var girl = _girlRepository.GetFirstById(id);
            if (girl.Author != user)
            {
                return Json(false);
            }

            girl.Name = name;
            _girlRepository.Update(girl);

            return Json(true);
        }
    }
}
