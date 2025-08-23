using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnderTheBridge.Data.Models;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models.UnderTheBridge;


namespace WebPortal.Controllers
{
    public class UnderTheBridgeController: Controller
    {
        private readonly IGuitarRepository Guitars;
        private readonly ICommentRepository Comments;
        private readonly IUserRepositrory Users;

        public UnderTheBridgeController(IGuitarRepository guitarRepository, ICommentRepository commentRepository, IUserRepositrory userRepository)
        {
            Guitars = guitarRepository;
            Comments = commentRepository;
            Users = userRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Catalog", "UnderTheBridge");
        }

        [HttpGet]
        public IActionResult Catalog()
        {
            var view = new List<GuitarViewModel>();

            view = Guitars.GetAllWithComments()
                .Select
                (
                    guitar => new GuitarViewModel
                    {
                        Id = guitar.Id,
                        Name = guitar.Name,
                        ImageUrl = guitar.ImageUrl,
                        Price = guitar.Price,
                        Status = guitar.Status,
                        Comments = guitar.Comments.Select(c => new CommentViewModel
                        {
                            Message = c.Message,
                            Mark = c.Mark,
                            Author = "",
                            CreatedAt = c.CreatedAt,
                        }).ToList()
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
        public IActionResult AddGuitar(GuitarCreateViewModel view)
        {
            var guitar = new GuitarEntity
            {
                Name = view.Name,
                ImageUrl = view.ImageUrl,
                Price = view.Price,
                Status = view.Status,
            };

            Guitars.Add(guitar);

            return RedirectToAction("Catalog");
        }

        [HttpGet]
        public IActionResult DelGuitar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DelGuitar(GuitarDeleteViewModel view)
        {
            var guitar = Guitars.GetById(view.Id);

            if (guitar == null)
            {
                throw new Exception("No such guitar");
            }

            Guitars.Remove(guitar);

            return RedirectToAction("Catalog");
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var view = new DetailViewModel();

            var guitar = Guitars.GetByIdWithCommentsAndAuthors(id);

            if (guitar == null)
            {
                throw new Exception("No such guitar");
            }

            view.Guitar = new GuitarViewModel
            {
                Id = guitar.Id,
                Name = guitar.Name,
                ImageUrl = guitar.ImageUrl,
                Price = guitar.Price,
                Status = guitar.Status,
                Comments = guitar.Comments.Select(c => new CommentViewModel
                {
                    Message = c.Message,
                    Mark = c.Mark,
                    Author = c.Author.UserName,
                    CreatedAt = c.CreatedAt,
                }).ToList()
            };

            view.CommentForm = new CommentCreateViewModel();

            return View(view);
        }

        [HttpPost]
        public IActionResult Detail(int id, CommentCreateViewModel commentForm)
        {
            var guitar = Guitars.GetByIdWithCommentsAndAuthors(id);
            if (guitar == null)
            {
                throw new Exception("No such product");
            }

            if (!ModelState.IsValid)
            {
                var view = new DetailViewModel();

                view.Guitar = new GuitarViewModel
                {
                    Id = guitar.Id,
                    Name = guitar.Name,
                    ImageUrl = guitar.ImageUrl,
                    Price = guitar.Price,
                    Status = guitar.Status,
                    Comments = guitar.Comments.Select(c => new CommentViewModel
                    {
                        Message = c.Message,
                        Mark = c.Mark,
                        Author = c.Author.UserName,
                        CreatedAt = c.CreatedAt,
                    }).ToList()
                };

                view.CommentForm = new CommentCreateViewModel();

                return View(view);
            }

            var comment = new CommentEntity
            {
                Message = commentForm.Message,
                Mark = commentForm.Mark,
                CreatedAt = DateTime.Now,
                Author = Users.GetAll().First() // change later on real user
            };

            guitar.Comments.Add(comment);
            Guitars.Update(guitar);

            return RedirectToAction("Detail", "UnderTheBridge", new { id });
        }

        [HttpGet]
        public IActionResult RelinkComment()
        {
            var view = new RelinkCommentViewModel();

            view.AllComments = Comments
                .GetAll()
                .Select(
                x => new SelectListItem
                {
                    Text = x.Message,
                    Value = x.Id.ToString()
                })
                .ToList();
            view.AllUsers = Users
                .GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.UserName,
                    Value = x.Id.ToString()
                })
                .ToList();

            return View(view);
        }

        [HttpPost]
        public IActionResult RelinkComment(RelinkCommentViewModel view)
        {
            var user = Users.GetFirstById(view.UserId);
            var comment = Comments.GetFirstById(view.CommentId);

            comment.Author = user;
            Comments.Update(comment);

            return RedirectToAction("RelinkComment");
        }
    }
}