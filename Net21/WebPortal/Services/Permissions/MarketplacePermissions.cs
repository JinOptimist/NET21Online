using WebPortal.DbStuff.Models;
using WebPortal.Enum;
using WebPortal.DbStuff.Models.Marketplace;
using WebPortal.Services;

namespace WebPortal.Services.Permissions
{
    public class MarketplacePermissions : IMarketplacePermissions
    {
        private AuthService _authService;

        public MarketplacePermissions(AuthService authService)
        {
            _authService = authService;
        }

        public bool CanDelete(Product product)
        {
            if (!_authService.IsAuthenticated())
            {
                return false;
            }

            var user = _authService.GetUser();
            return user.Role == Role.Admin || user.Role == Role.MarketplaceModerator;
        }

        public bool CanAdd()
        {
            if (!_authService.IsAuthenticated())
            {
                return false;
            }

            var user = _authService.GetUser();
            return user.Role == Role.Admin || user.Role == Role.MarketplaceModerator;
        }
    }
}