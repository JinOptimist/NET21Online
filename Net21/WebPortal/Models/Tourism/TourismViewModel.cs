using System.ComponentModel.DataAnnotations;
using WebPortal.Models.CustomValidationAttributtes;

namespace WebPortal.Models.Tourism
{
    public class TourismViewModel
    {
        [Required(ErrorMessage = "The Days for Title is required")]
        [MinMax(1,12)]
        public int? Days { get; set; }

        [Required(ErrorMessage = "The Title Name is required")]
        [TitleName("travel")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "The Image URL for Title is required")]
        public string? URL { get; set; }
        public int Id { get; set; }
    }
}
