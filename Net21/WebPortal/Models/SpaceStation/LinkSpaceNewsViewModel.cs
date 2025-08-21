using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebPortal.Models
{
    public class LinkSpaceNewsViewModel
    {

        public int AuthorId { get; set; }
        public List<SelectListItem> AllUsers { get; set; } = new List<SelectListItem>();
        public int SpaceNewsId { get; set; }
        public List<SelectListItem> AllSpaceNews { get; set; } = new List<SelectListItem>();
    }
}