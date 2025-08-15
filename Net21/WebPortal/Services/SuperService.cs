using WebPortal.DbStuff;
using WebPortal.DbStuff.Repositories;

namespace WebPortal.Services
{
    public class SuperService
    {
        private UserRepositrory _userRepositrory;
        private GirlRepository _girlRepository;
        private WebPortalContext _webPortalContext;

        public SuperService(UserRepositrory userRepositrory, GirlRepository girlRepository, WebPortalContext webPortalContext)
        {
            _userRepositrory = userRepositrory;
            _girlRepository = girlRepository;
            _webPortalContext = webPortalContext;
        }
    }
}
