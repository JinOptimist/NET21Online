using System.ComponentModel.DataAnnotations;
using WebPortal.Models.CustomValidationAttributtes;

namespace WebPortal.Models.UnderTheBridge
{
    public class CommentCreateViewModel
    {
        [Required(ErrorMessage = "You can't left empty comment")]
        [MaxLength(500, ErrorMessage = "Limit is 500 laters")]
        [AntiBot]
        public string Message { get; set; }

        [Required]
        [MinMax(1, 4, ErrorMessage = "I don't know how, but you set mark more then 5 or less then 1")] // max 4 is seted only for tests
        public int Mark { get; set; }
    }
}
