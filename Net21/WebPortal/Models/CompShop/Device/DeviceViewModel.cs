using System.ComponentModel.DataAnnotations;
using WebPortal.DbStuff.Models.CompShop;
using WebPortal.DbStuff.Models.CompShop.Devices;

namespace WebPortal.Models.CompShop.Device
{
    public class DeviceViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название товара")]
        public string? Name { get; set; }

        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Display(Name = "Тип устройства")]
        public int TypeDeviceId { get; set; }

        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [Display(Name = "Цена (BYN)")]
        public double? Price { get; set; }

        [Display(Name = "Ссылка на изображение")]
        public string? Image { get; set; }

        [Display(Name = "Будет ли отображаться на главной странице")]
        public bool IsPopular { get; set; }

        public TypeDevice TypeDevice { get; set; }
        public Category Category { get; set; }

        public int ComputerId { get; set; }
    }
}
