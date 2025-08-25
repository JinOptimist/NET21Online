using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebPortal.Models.CustomValidationAttributtes;

namespace WebPortal.Models.SpaceStation
{
    public class SpaceNewsViewModel
    {
        public int Id { get; set; }

        [Url(ErrorMessage = "Please enter a valid image URL.")]
        [StringLength(500, ErrorMessage = "Image URL must not exceed 500 characters.")]
        public string ImageUrl { get; set; }

        [IsUniqTitleName]
        [Required(ErrorMessage = "News title is required")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "The title must contain from 5 to 200 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The content of the news is required to be filled in")]
        [MinLength(50, ErrorMessage = "The content of the news must contain at least 50 characters.")]
        public string Content { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public string AuthorName { get; set; }
        public int AuthorId { get; set; }
        public List<SelectListItem> AllUsers { get; set; } = new List<SelectListItem>();
    }
}