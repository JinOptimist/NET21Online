using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortal.Enum;

namespace WebPortal.Models.Festival
{
    public class FestivalViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public FestivalTheme Theme { get; set; }
        public string LogoUrl { get; set; }
        public int GirlsCount { get; set; }
    }

    public class FestivalCreationViewModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string? Slogan { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public FestivalTheme Theme { get; set; }

        [Required]
        [Url]
        public string LogoUrl { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }
    }

    public class FestivalLinkGirlsViewModel
    {
        public int FestivalId { get; set; }
        public string FestivalName { get; set; }
        public List<int> SelectedGirlIds { get; set; } = new List<int>();
        public List<SelectListItem> AllGirls { get; set; } = new List<SelectListItem>();
    }
}
