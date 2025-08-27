using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebPortal.Models.CustomValidationAttributtes;

namespace WebPortal.Models.Tourism
{
    public class TourViewModel
    {
        public int Id { get; set; }
        public string TourImg { get; set; }
        public string TourName { get; set; }       
        public string? AuthorName { get; set; }
    }
}
