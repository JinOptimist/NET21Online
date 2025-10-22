using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebPortal.Models.CustomValidationAttributtes;

namespace WebPortal.Models.Girls
{
    public class GirlViewModel
    {
        public int Id { get; set; }

        public string? Src { get; set; }

        public string? Name { get; set; }

        public DateTime CreationTime { get; set; }

        public string AuthorName { get; set; }
        public int AuthorId { get; set; }
        public bool CanDelete { get; set; }
        public List<SelectListItem> AllUsers { get; set; } = new List<SelectListItem>();
        
        // Все поля из Girl
        public int Age { get; set; }
        public int? Size { get; set; }
        public string Url { get; set; }
        
        // Все поля из User (Author)
        public string AuthorUserName { get; set; }
        public string AuthorPassword { get; set; }
        public string AuthorAvatarUrl { get; set; }
        public int AuthorMoney { get; set; }
        public string AuthorRole { get; set; }
        public string AuthorLanguage { get; set; }
    }
}
