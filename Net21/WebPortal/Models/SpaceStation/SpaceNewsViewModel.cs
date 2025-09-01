using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebPortal.Models.CustomValidationAttributtes;

namespace WebPortal.Models.SpaceStation
{
    public class SpaceNewsViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public string AuthorName { get; set; }
        public int AuthorId { get; set; }
        public bool CanRemove { get; set; }
        public List<SelectListItem> AllUsers { get; set; } = new List<SelectListItem>();
    }
}