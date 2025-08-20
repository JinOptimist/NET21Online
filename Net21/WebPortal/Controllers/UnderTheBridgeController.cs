using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnderTheBridge.Data.Models;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models.Marketplace.BaseItem;
using WebPortal.DbStuff.Repositories;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models.UnderTheBridge;


namespace WebPortal.Controllers
{
    public class UnderTheBridgeController: Controller
    {
        private readonly IGuitarRepository Guitars;
        private readonly ICommentRepository Comments;
        private static int step = 0;

        public UnderTheBridgeController(IGuitarRepository guitarRepository, ICommentRepository commentRepository)
        {
            Guitars = guitarRepository;
            Comments = commentRepository;
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

            step++;
            Console.WriteLine(step);
            Console.WriteLine("id: " + id);

            var guitar = Guitars.GetByIdWithComments(id);

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
            var guitar = Guitars.GetByIdWithComments(id);

            if (guitar == null)
            {
                throw new Exception("No such product");
            }

            comment.Author = "Me";
            comment.CreatedAt = DateTime.Now;

            guitar.Comments.Add(comment);

            Guitars.Update(guitar);

            return RedirectToAction("Detail", "UnderTheBridge", new { id });
        }
    }
}