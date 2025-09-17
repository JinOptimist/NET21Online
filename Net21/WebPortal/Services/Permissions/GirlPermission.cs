using WebPortal.DbStuff.Models;
using WebPortal.Enum;

namespace WebPortal.Services.Permissions
{
    public class GirlPermission : IGirlPermission
    {
        private IAuthService _authService;

        public GirlPermission(IAuthService authService)
        {
            _authService = authService;
        }

        public bool CanDelete(Girl girl)
        {
            if (!_authService.IsAuthenticated())
            {
                return false;
            }

            var user = _authService.GetUser();
            if (user.Role == Role.Admin
                || user.Role == Role.GrilModrator)
            {
                return true;
            }

            return girl.Author?.Id == user.Id;
        }
    }
}
