using System.ComponentModel.DataAnnotations;
using WebPortal.Models.CustomValidationAttributtes.CoffeShop;

namespace WebPortal.Models.CoffeShop
{
    public class UserCommentViewModel
    {
        //Задать вопрос по поводу бд 
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Img Need")]
        public string ImgUser { get; set; }
        
        [Required(ErrorMessage = "Name Need")]
        [IsUniqNameUser]
        public string NameUser { get; set; }
        
        [Required(ErrorMessage = "Description Need")]
        [MaxLength(50)]
        [HateWords]
        public string Description { get; set; }
    }
}
