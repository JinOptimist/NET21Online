using WebPortal.DbStuff.Models.CoffeShop;

namespace WebPortal.Services.Permissions.CoffeShop
{
    public interface ICoffeShopPermision
    {
        bool CanFindPage(CoffeeProduct coffeeProduct);
    }
}