namespace WebPortal.Models.CoffeShop
{
    public class CoffeeShopViewModel
    {
        //Задать вопрос по поводу бд 
        public List<CoffeeProductViewModel> CoffeeProducts { get; set; }
        public List<UserCommentViewModel> UserComments { get; set; }
    }
}
