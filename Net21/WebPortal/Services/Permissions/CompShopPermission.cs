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

            return _authService.GetName();
        }
    }
}