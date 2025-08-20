namespace WebPortal.Models.CoffeShop
{
    public class CoffeeProductViewModel
    {
        public int Id { get; set; }
        public string Img{ get; set; }
        public string Name { get; set; }
        public int Cell { get; set; }

        public int AuthorId { get; set; }            // Id ������������
        public string AuthorName { get; set; }       // ��� ������ (��� �����������)

        public List<UserCoffeShopViewModel> AvailableAuthors { get; set; }
            = new List<UserCoffeShopViewModel>();
    }
}
