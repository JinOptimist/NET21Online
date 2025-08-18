using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models.HelpfullModels;
using WebPortal.Models;

namespace WebPortal.Controllers
{
    public class HelpfullController : Controller
    {
        private WebPortalContext _webPortalContext;

        public HelpfullController(WebPortalContext context)
        {
            _webPortalContext = context;
        }

        public IActionResult Index()
        {
            var suggestsFromDataBases = _webPortalContext.Suggests.ToList();
            var linksViewModel = new List<LinkViewModel>();
            linksViewModel = suggestsFromDataBases
                .Select(suggestFromDataBase => new LinkViewModel
                {
                    Text = suggestFromDataBase.Advise,
                    Url = suggestFromDataBase.Url,
                })
                .ToList();

            return View(linksViewModel);
        }

        public IActionResult Csharp()
        {
            return View();
        }

        // Show me the page
        [HttpGet]
        public IActionResult AddPage()
        {
            return View();
        }

        // Save to Database
        [HttpPost]
        public IActionResult AddPage(string advice, string url)
        {
            var suggest = new Suggest()
            {
                Advise = advice,
                Url = url
            };
            _webPortalContext.Suggests.Add(suggest); // Remember my plan
            
            _webPortalContext.SaveChanges(); // Send to database

            return View();
        }
    }
}
