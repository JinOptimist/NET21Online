using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.IO.Pipes;
using WebPortal.Models.Cartoon;

namespace WebPortal.Controllers
{
    public class CartoonController : Controller
    {
        private static List<CartoonViewModel> Cartoon = new List<CartoonViewModel>();
        public IActionResult Index()
        {
            if (!Cartoon.Any())
            {
                for (int i = 0; i < 4; i++)
                {
                    var cartoonNames = new[]
                    {
                        "Красавица и Чудовище",
                        "Вверх",
                        "Моана",
                        "Корпорация монстров",
                    };

                    var viewCartoon = new CartoonViewModel()
                    {
                        Name = cartoonNames[i],

                        Src = $"/images/cartoon/Image{i}.jpeg",
                    };
                    Cartoon.Add(viewCartoon);
                }
            }

            return View(Cartoon);
        }
    }
}
