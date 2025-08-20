using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UnderTheBridge.Data.Models;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories;
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
            var view = new CatalogViewModel();

            view.Guitars = Guitars.GetAllWithComments();

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
            var guitar = view.Guitar;

            Guitars.Add(guitar);

            return RedirectToAction("Catalog");
        }

        [HttpGet]
        public IActionResult DelGuitar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DelGuitar(DelGuitarViewModel view)
        {
            var guitarId = view.Id;

            var guitar = Guitars.GetById(guitarId);

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

            view.Guitar = guitar;

            return View(view);
        }

        [HttpPost]
        public IActionResult Detail(int id, CommentEntity comment)
        {
            var guitar = Guitars.GetByIdWithCommentsAndAuthors(id);

            if (guitar == null)
            {
                throw new Exception("No such product");
            }

            comment.CreatedAt = DateTime.Now;
            comment.Author = Users.GetAll().First(); // change later on real user

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