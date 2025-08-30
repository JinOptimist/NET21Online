using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebPortal.Models.CustomValidationAttributtes;

namespace WebPortal.Models.Tourism
{
    public class PersonalHomeViewModel
    {
        public int Id { get; set; }    
        public string Name { get; set; }

        public List<TourViewModel> Tours { get; set; } = new List<TourViewModel>();
        public List<TourPreviewViewModel> TourPreviews { get; set; } = new List<TourPreviewViewModel>();
    }
}
