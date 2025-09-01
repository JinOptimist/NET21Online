using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Models.CompShop.Devices;
using WebPortal.Enum;

namespace WebPortal.Services.Permissions
{
    public class CompShopPermission
    {
        private readonly AuthService _authService;

        public CompShopPermission(AuthService authService)
        {
            _authService = authService;
        }

        public bool CanDelete()
        {
            if (!_authService.IsAuthenticated())
            {
                return false;
            }

           return _authService.GetUser().Role == Role.Admin;
        }

        public string GetNameUser()
        {
            if (!_authService.IsAuthenticated())
            {
                return "Войти";
            }

            var user = _authService.GetUser();

            return user.UserName;
        }
    }
}
