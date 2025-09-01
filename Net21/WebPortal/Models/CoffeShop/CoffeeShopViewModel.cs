namespace WebPortal.Models.CoffeShop
{
    public class CoffeeShopViewModel
    {
        public HomeCoffeShopViewModel LayoutModelUser { get; set; }
        public List<CoffeeProductViewModel> CoffeeProducts { get; set; }
        public List<UserCommentViewModel> UserComments { get; set; }

    }
}
