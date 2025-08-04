using Microsoft.AspNetCore.Mvc;

namespace WebPortal.Controllers
{
    public class GirlController : Controller
    {
        public IActionResult Index()
        {
            var girlUrls = new List<string>()
            {
                "/images/girls/girl1.jpg",
                "/images/girls/girl2.webp",
                "/images/girls/girl3.jpg",
                "/images/girls/girl4.jpg",
                "/images/girls/girl5.webp",
            };
            girlUrls.AddRange(girlUrls);
            return View(girlUrls);
        }
    }
}
