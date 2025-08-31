using System.ComponentModel.DataAnnotations;

namespace WebPortal.Models.Cartoon
{
    public class CartoonViewModel
    {
        public int Id { get; set; }
        
        [Required]
        public string? Name { get; set; }
        
        public string? Src { get; set; }
        
        public string? Description { get; set; }
        
        public int ReleaseYear { get; set; }
        
        public int EpisodeCount { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public bool IsActive { get; set; } = true;
    }
}