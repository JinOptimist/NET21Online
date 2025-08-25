namespace WebPortal.DbStuff.Models.CoffeShop
{
    public class UserCoffeShop : BaseModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AvatarUrl { get; set; }
        public virtual List<CoffeeProduct> CreatedCoffe { get; set; } = new List<CoffeeProduct>();
    }
}
