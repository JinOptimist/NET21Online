using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models.marketplace.BaseViewModel;
using WebPortal.Models.UnderTheBridge;
using WebPortal.Services;
using WebPortal.Services.Permissions;


namespace WebPortal.Controllers
{
    public class UnderTheBridgeController : Controller
    {
        private readonly IGuitarRepository Guitars;
        private readonly ICommentRepository Comments;
        private readonly IUserRepositrory Users;

        private readonly ICommentPermission _commentPermission;

        private IAuthService _authService;

        public UnderTheBridgeController(IGuitarRepository guitarRepository, ICommentRepository commentRepository, IUserRepositrory userRepository, IAuthService authService, ICommentPermission commentPermission)
        {
            Guitars = guitarRepository;
            Comments = commentRepository;
            Users = userRepository;

            _authService = authService;
            
            _commentPermission = commentPermission;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Catalog", "UnderTheBridge");
        }

        [HttpGet]
        public IActionResult Catalog()
        {
            var view = new CatalogViewModel();

            view.Guitars = Guitars.GetAllWithComments()
                .Select
                (
                    g => new GuitarViewModel
                    {
                        Id = g.Id,
                        Name = g.Name,
                        Image = g.ImageUrl,
                        Price = g.Price,
                        Status = g.Status,
                        CommentMarks = g.Comments
                            .Select
                            (
                                c => c.Mark
                            ).ToList()
                    }
                ).ToList();

            return View(view);
        }

        [HttpGet]
        public IActionResult AddGuitar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGuitar(AddGuitarViewModel view)
        {
            var guitar = new GuitarEntity
            {
                Name = view.Name,
                ImageUrl = view.Image,
                Price = view.Price,
                Status = view.Status,
            };

            Guitars.Add(guitar);

            return RedirectToAction("Catalog");
        }

        //[HttpGet]
        //public IActionResult DelGuitar()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult DelGuitar(DelGuitarViewModel view)
        //{
        //    var guitarId = view.Id;

        //    var guitar = Guitars.GetById(guitarId);

        //    if (guitar == null)
        //    {
        //        throw new Exception("No such guitar");
        //    }

        //    Guitars.Remove(guitar);

        //    return RedirectToAction("Catalog");
        //}

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var view = new DetailViewModel();

            var guitar = Guitars.GetById(id);

            if (guitar == null)
            {
                throw new Exception("No such guitar");
            }

            view.Guitar = new GuitarViewModel
            {
                Id = guitar.Id,
                Name = guitar.Name,
                Image = guitar.ImageUrl,
                Price = guitar.Price,
                Status = guitar.Status,
                CommentMarks = guitar.Comments
                .Select
                (
                    c => c.Mark
                ).ToList()
            };

            view.Comments = Comments.GetByGuitarId(id)
                .Select
                (
                    c => new CommentViewModel
                    {
                        Id = c.Id,
                        Message = c.Message,
                        Mark = c.Mark,
                        CreatedAt = c.CreatedAt,
                        AuthorName = c.Author.UserName,
                        CanDelete = _commentPermission.CanDelete(c)
                    }
                ).ToList();

            view.IsAuthenticated = _authService.IsAuthenticated();

            return View(view);
        }

        [HttpPost]
        public IActionResult Detail(int id, DetailViewModel view)
        {
            if (!_authService.IsAuthenticated())
            {
                throw new Exception("Not authenticated user tring to create a comment");
            }

            var guitar = Guitars.GetById(id);

            if (guitar == null)
            {
                throw new Exception("No such guitar");
            }

            var form = view.CommentForm;

            var newComment = new CommentEntity()
            {
                Message = form.Message,
                Mark = form.Mark,
                CreatedAt = DateTime.Now,
                Author = _authService.GetUser()
            };

            guitar.Comments.Add(newComment);

            Guitars.Update(guitar);

            return RedirectToAction("Detail", "UnderTheBridge", new { id });
        }

        //[HttpGet]
        //public IActionResult RelinkComment()
        //{
        //    var view = new RelinkCommentViewModel();

        //    view.AllComments = Comments
        //        .GetAll()
        //        .Select(
        //        x => new SelectListItem
        //        {
        //            Text = x.Message,
        //            Value = x.Id.ToString()
        //        })
        //        .ToList();
        //    view.AllUsers = Users
        //        .GetAll()
        //        .Select(x => new SelectListItem
        //        {
        //            Text = x.UserName,
        //            Value = x.Id.ToString()
        //        })
        //        .ToList();

        //    return View(view);
        //}

        //[HttpPost]
        //public IActionResult RelinkComment(RelinkCommentViewModel view)
        //{
        //    var user = Users.GetFirstById(view.UserId);
        //    var comment = Comments.GetFirstById(view.CommentId);

        //    comment.Author = user;
        //    Comments.Update(comment);

        //    return RedirectToAction("RelinkComment");
        //}

        [HttpGet]
        public IActionResult DeleteComment(int id)
        {
            var comment = Comments.GetFirstById(id);

            if (!_authService.IsAuthenticated())
            {
                throw new Exception("User isn't authenticated, but he is tring to delete comment");
            }
            if (!_commentPermission.CanDelete(comment))
            {
                throw new Exception("This user can't delete comment");
            }

            Comments.Remove(id);

            return RedirectToAction("Detail", "UnderTheBridge", new { id = comment.GuitarId });
        }
    }
}