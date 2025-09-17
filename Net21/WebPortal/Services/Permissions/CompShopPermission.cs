using WebPortal.Enum;
using WebPortal.Services.Permissions.Interface;

namespace WebPortal.Services.Permissions
{
    public class CompShopPermission : ICompShopPermission
    {
        private readonly IAuthService _authService;

        public CompShopPermission(IAuthService authService)
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

            return _authService.GetName();
        }
    }
}