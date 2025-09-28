using System.ComponentModel.DataAnnotations;

namespace ProductsMinimalApi.DTOs.Models
{
    public class UpdateProductDto
    {
        [Required]
        int Id { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal? Price { get; set; }

        [StringLength(50)]
        public string? Category { get; set; }

        [Url]
        public string? ImageUrl { get; set; }
    }
}