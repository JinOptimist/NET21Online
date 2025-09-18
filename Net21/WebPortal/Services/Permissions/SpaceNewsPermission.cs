using WebPortal.DbStuff.Models;
using WebPortal.Enum;

namespace WebPortal.Services.Permissions
{
    public class SpaceNewsPermission : ISpaceNewsPermission
    {
        private AuthService _authService;

        public SpaceNewsPermission(AuthService authService)
        {
            _authService = authService;
        }

        public bool CanRemove(SpaceNews spaceNews)
        {
            if (!_authService.IsAuthenticated())
            {
                return false;
            }
            var user = _authService.GetUser();
            if (user.Role == Role.Admin
                || user.Role == Role.SpaceNewsModerator)
            {
                return true;
            }
            return spaceNews.Author?.Id == user.Id;
        }
    }
}
