using WebPortal.DbStuff.Models.CoffeShop;
using WebPortal.Enum;

namespace WebPortal.Services.Permissions.CoffeShop
{
    public class CoffeShopPermision : ICoffeShopPermision
    {
        private IAuthService _authService;

        public CoffeShopPermision(IAuthService authService)
        {
            _authService = authService;
        }

        public bool CanFindPage(CoffeeProduct coffeeProduct)
        {
            if (!_authService.IsAuthenticated())
            {
                return false;
            }
            var user = _authService.GetUser();
            if (user.Role == Role.Admin ||
                user.Role == Role.CoffeProductModerator)
            {
                return true;

            }

            return coffeeProduct.AuthorAdd?.Id == user.Id;
        }

    }
}
