using System.ComponentModel.DataAnnotations;
using WebPortal.DbStuff.Models.CompShop;
using WebPortal.DbStuff.Models.CompShop.Devices;
using WebPortal.Models.CustomValidationAttributtes;
using WebPortal.Models.CustomValidationAttributtes.CompShop;
using WebPortal.Services.Permissions;

namespace WebPortal.Models.CompShop.Device
{
    public class DeviceViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название товара")]
        [Required(ErrorMessage = "Поле не заполнено!")]
        [MinLength(3, ErrorMessage = "Минимальная длина 3 символа!")]
        [InUniqCompShopName]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Поле не заполнено!")]
        [MinLength(10, ErrorMessage = "Минимальная длина 10 символов!")]
        public string Description { get; set; }

        [Display(Name = "Тип устройства")]
        public int TypeDeviceId { get; set; }

        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [Display(Name = "Цена (BYN)")]
        [Required(ErrorMessage = "Поле не заполнено!")]
        public double Price { get; set; }

        [Display(Name = "Ссылка на изображение")]
        [Required(ErrorMessage = "Поле не заполнено!")]
        public string Image { get; set; }

        [Display(Name = "Будет ли отображаться на главной странице")]
        public bool IsPopular { get; set; }

        public TypeDevice TypeDevice { get; set; }
        public Category Category { get; set; }

        public int? ComputerId { get; set; }

        public bool CanDelete { get; set; }
    }
}
