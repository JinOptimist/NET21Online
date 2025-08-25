using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.Services
{
    public class AuthService
    {
        private IHttpContextAccessor _contextAccessor;
        private IUserRepositrory _userRepositrory;

        public AuthService(IHttpContextAccessor contextAccessor, IUserRepositrory userRepositrory)
        {
            _contextAccessor = contextAccessor;
            _userRepositrory = userRepositrory;
        }

        public int GetId()
        {
            var httpContext = _contextAccessor.HttpContext;
            return int.Parse(httpContext
                .User
                .Claims
                .First(x => x.Type == "Id")
                .Value); ;
        }

        public User GetUser()
        {
            return _userRepositrory.GetFirstById(GetId());
        }
        
        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext!.User?.Identity?.IsAuthenticated ?? false; 
        }
    }
}
