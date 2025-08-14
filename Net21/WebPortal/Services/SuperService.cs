using WebPortal.DbStuff;
using WebPortal.DbStuff.Repositories;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.Services
{
    public class SuperService
    {
        private UserRepositrory _userRepositrory;
        private GirlRepository _girlRepository;
        private WebPortalContext _webPortalContext;
        private CdekRepository _cdekRepository;

        public SuperService(UserRepositrory userRepositrory, GirlRepository girlRepository, WebPortalContext webPortalContext, CdekRepository cdekRepository)
        {
            _userRepositrory = userRepositrory;
            _girlRepository = girlRepository;
            _webPortalContext = webPortalContext;
            _cdekRepository = cdekRepository;
        }
    }
}
