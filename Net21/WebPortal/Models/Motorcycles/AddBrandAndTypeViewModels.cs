using System.ComponentModel.DataAnnotations;
using WebPortal.Models.CustomValidationAttributtes;

namespace WebPortal.Models.Motorcycles
{
    public class AddBrandAndTypeViewModels
    {
        [MaxLength(50)]
        [IsUniqMotorcycleBrand]
        public string BrandName { get; set; }
        
        [MaxLength(50)]
        [IsUniqMotorcycleType]
        public string TypeName { get; set; }
        
        public string Description { get; set; }
    }
}
