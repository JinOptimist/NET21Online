using System.ComponentModel.DataAnnotations;

namespace WebPortal.Models.Marketplace
{
    public class MarketplaceProductAddViewModel
    {
        [Required(ErrorMessage = "Тип товара обязателен")]
        public string ProductType { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина названия от 3 до 100 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Бренд обязателен")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Цена обязательна")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть больше 0")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        [Url(ErrorMessage = "Некорректный URL изображения")]
        public string ImageUrl { get; set; }
        public string Processor { get; set; }

        [Range(1, 128, ErrorMessage = "RAM должен быть от 1 до 128 ГБ")]
        public int? RAM { get; set; }
        public string OS { get; set; }

        public List<string> AvailableProductTypes { get; } = new List<string>
        {
            "Laptop",
            "Smartphone",
            "SmartWatch",
            "Headphones"
        };
    }
}