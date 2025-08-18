using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebPortal.Models.Girls
{
    public class GirlViewModel
    {
        public int Id { get; set; }
        public string? Src { get; set; }
        public string? Name { get; set; }
        public int? Rating { get; set; }
        public DateTime CreationTime { get; set; }

        public string AuthorName { get; set; }
        public int AuthorId { get; set; }
        public List<SelectListItem> AllUsers { get; set; } = new List<SelectListItem>();
    }
}
