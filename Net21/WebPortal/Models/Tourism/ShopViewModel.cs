using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebPortal.Models.CustomValidationAttributtes;

namespace WebPortal.Models.Tourism
{
    public class ShopViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Url of Image Tour is required")]
        public string TourImg { get; set; }

        [Required(ErrorMessage = "The Tour Name is required")]
        [MaxLength(40)]
        public string TourName { get; set; }

        [OneFieldReqired]
        public string? Author { get; set; }

        [OneFieldReqired]
        public int? AuthorId { get; set; }
        public List<SelectListItem> AllUsers { get; set; } = new List<SelectListItem>();
    }
}
