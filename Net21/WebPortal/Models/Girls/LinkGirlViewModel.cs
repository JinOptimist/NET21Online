using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebPortal.Models.Girls
{
    public class LinkGirlViewModel
    {
        public int AuthorId { get; set; }
        public List<SelectListItem> AllUsers { get; set; } = new List<SelectListItem>();

        public int GirlId { get; set; }
        public List<SelectListItem> AllGirls { get; set; } = new List<SelectListItem>();
    }
}
