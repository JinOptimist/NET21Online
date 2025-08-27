using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebPortal.Models.Tourism
{
    public class LinkTourWithAuthorViewModel
    {
        public int AuthorId { get; set; }
        public List<SelectListItem> AllUsers { get; set; } = new List<SelectListItem>();

        public int TitleNameId { get; set; }
        public List<SelectListItem> AllShopItems { get; set; } = new List<SelectListItem>();
    }
}
