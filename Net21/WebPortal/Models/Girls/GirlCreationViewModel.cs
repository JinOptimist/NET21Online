using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebPortal.Localizations;
using WebPortal.Models.CustomValidationAttributtes;

namespace WebPortal.Models.Girls
{
    public class GirlCreationViewModel
    {
        [Required(ErrorMessage = "I don't beleavi in empty url")]
        public string? Src { get; set; }

        [Required(ErrorMessage = "There is no girl without name")]
        [MaxLength(10)]
        //[HateApple(ErrorMessage = "Name with Apple it's strage")]
        [HateApple(
            ErrorMessageResourceType = typeof(Girl), 
            ErrorMessageResourceName = nameof(Girl.ValidationMessage_HateApple))]
        [IsNiceEnought]
        [InUniqGirlName]
        public string? Name { get; set; }

        [MinMax(1, 10)]
        public int? Rating { get; set; }

        public int AuthorId { get; set; }
        public List<SelectListItem> AllUsers { get; set; } = new List<SelectListItem>();
    }
}
