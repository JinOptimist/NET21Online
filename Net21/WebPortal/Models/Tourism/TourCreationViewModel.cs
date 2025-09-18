using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebPortal.Models.CustomValidationAttributtes;
using WebPortal.Localizations;
using WebPortal.DbStuff.DataModels;

namespace WebPortal.Models.Tourism
{
    public class TourCreationViewModel
    {

        [Required(ErrorMessageResourceType =typeof(TourismLoc), ErrorMessageResourceName = nameof(TourismLoc.Validation_Url_Required))]
        public IFormFile TourImgFile { get; set; }

        [Required(ErrorMessageResourceType = typeof(TourismLoc), ErrorMessageResourceName = nameof(TourismLoc.Validation_Name_Required))]
        [MaxLength(40)]
        [IsUniqTourName]
        public string TourName { get; set; }

        [Required(ErrorMessageResourceType = typeof(TourismLoc), ErrorMessageResourceName = nameof(TourismLoc.Validation_Author_Required))]
        public int AuthorId { get; set; }
        public List<SelectListItem> AllUsers { get; set; } = new List<SelectListItem>(); 
        public DateTime DateTime { get; set; }  = DateTime.Now;
        public List<ToursAutorStatiscticModel> ToursStatiscticModel { get; set; }
    }
}
