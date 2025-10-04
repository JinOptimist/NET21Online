using WebPortal.DbStuff;
using WebPortal.DbStuff.Repositories;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Services.AutoRegistrationInDI;

namespace WebPortal.Services
{
    public class SuperService
    {
        private IUserRepositrory _userRepositrory;
        private IGirlRepository _girlRepository;
        private WebPortalContext _webPortalContext;

        public SuperService(int test)
        {

        }

        [AutoResolve]
        public SuperService(
            IUserRepositrory userRepositrory,
            IGirlRepository girlRepository,
            WebPortalContext webPortalContext)
        {
            _userRepositrory = userRepositrory;
            _girlRepository = girlRepository;
            _webPortalContext = webPortalContext;
        }
    }
}
