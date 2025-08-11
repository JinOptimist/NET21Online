using Microsoft.AspNetCore.Mvc;
using WebPortal.Models;
using System.Collections.Generic;

namespace WebPortal.Controllers
{
    public class SpaceStationController : Controller
    {
        private static List<SpaceNewsModel> NewsItems = new List<SpaceNewsModel>();

        public IActionResult Index()
        {
            return View(NewsItems);
        }

        [HttpGet]
        public IActionResult News()
        {
            return View();
        }

        [HttpPost]
        public IActionResult News(string imageUrl, string title, string content)
        {
            if (!string.IsNullOrWhiteSpace(imageUrl))
            {
                NewsItems.Add(new SpaceNewsModel
                {
                    ImageUrl = imageUrl,
                    Title = title,
                    Content = content
                });
            }

            return RedirectToAction("Index");
        }
    }
}