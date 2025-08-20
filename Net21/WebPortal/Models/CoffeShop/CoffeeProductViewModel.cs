namespace WebPortal.Models.CoffeShop
{
    public class CoffeeProductViewModel
    {
        public int Id { get; set; }
        public string Img{ get; set; }
        public string Name { get; set; }
        public int Cell { get; set; }

        public int AuthorId { get; set; }            // Id пользователя
        public string AuthorName { get; set; }       // Имя автора (для отображения)

        public List<UserCoffeShopViewModel> AvailableAuthors { get; set; }
            = new List<UserCoffeShopViewModel>();
    }
}
