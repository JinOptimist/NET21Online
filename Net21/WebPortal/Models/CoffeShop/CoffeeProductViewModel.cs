using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebPortal.Models.CoffeShop
{
    public class CoffeeProductViewModel
    {
        public int Id { get; set; }
        public string Img{ get; set; }
        public string Name { get; set; }
        public int Cell { get; set; }

        public int AuthorId { get; set; }            
        public string AuthorName { get; set; }
        
        public bool CanFindPage { get; set; }

        public List<SelectListItem> AvailableAuthors { get; set; } = new List<SelectListItem>();
        public List<string> GalleryImages { get; set; } = new List<string>();
    }
}
