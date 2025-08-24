using System.ComponentModel.DataAnnotations;

namespace WebPortal.Models.Motorcycles
{
    public class MotorcyclesViewModel
    {
        [Required]
        public string? Src {  get; set; }
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        public int ? Id { get; set; }
    }
}
