using WebPortal.DbStuff.Models.Tourism;
using WebPortal.DbStuff.Repositories;
using WebPortal.Enum;

namespace WebPortal.Services.Permissions
{
    public class TourPermission : ITourPermission
    {
        private IAuthService _authService;

        public TourPermission(IAuthService authService)
        {
            _authService = authService;
        }

        public bool CanDelete(Tours tour)
        {
            if (!_authService.IsAuthenticated())
            {
                return false;
            }

            var user = _authService.GetUser();
            var userRightForDeletion = user.Role == Role.Admin || tour.Author?.Id == user.Id;
            return userRightForDeletion;
        }
    }
}
