using WebPortal.Models.CompShop.Devices;

namespace WebPortal.Models.CompShop
{
    public class NewsViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Text { get; set; }

        public string? Image { get; set; }

        public string DateCreate => DateTime.Now.ToString("dd.MM.yyyy");
    }
}
