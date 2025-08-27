using System.ComponentModel.DataAnnotations;

namespace WebPortal.Models.Motorcycles
{
    public class MotorcyclesViewModel
    {
        public string? Src {  get; set; } 
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int ? Id { get; set; }
    }
}
