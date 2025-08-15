using System.ComponentModel.DataAnnotations;
namespace WebPortal.DbStuff.Models;

public class CdekCallRequest : BaseModel
    {
        [Required(ErrorMessage = "Укажите имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Задайте вопрос")]
        public string Question { get; set; }

        [Range(1000000000, 9999999999, ErrorMessage = "Некорректный номер")]
        public int PhoneNumber { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now; // Автозаполнение
    }
