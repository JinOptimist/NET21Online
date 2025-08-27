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
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина бренда от 2 до 50 символов")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Цена обязательна")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть больше 0")]
        public decimal Price { get; set; }

        [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        public string Description { get; set; }

        [Url(ErrorMessage = "Некорректный URL изображения")]
        public string ImageUrl { get; set; }

        [ProductTypeRequired("Laptop", ErrorMessage = "Процессор обязателен для ноутбуков")]
        public string Processor { get; set; }

        [ProductTypeRequired("Laptop", ErrorMessage = "Оперативная память обязательна для ноутбуков")]
        [Range(1, 128, ErrorMessage = "RAM должен быть от 1 до 128 ГБ")]
        public int? RAM { get; set; }

        [ProductTypeRequired("Laptop", ErrorMessage = "Операционная система обязательна для ноутбуков")]
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