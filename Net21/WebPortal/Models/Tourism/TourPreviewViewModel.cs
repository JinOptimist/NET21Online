using System.ComponentModel.DataAnnotations;
using WebPortal.Models.CustomValidationAttributtes;

namespace WebPortal.Models.Tourism
{
    public class TourPreviewViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Days for Title is required")]
        [MinMax(1,12)]
        public int? Days { get; set; }

        [Required(ErrorMessage = "The Title Name is required")]
        [RequiredWord("travel")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "The Image URL for Title is required")]
        public string? URL { get; set; }
    }
}
