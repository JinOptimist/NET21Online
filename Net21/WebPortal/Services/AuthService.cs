using Microsoft.AspNetCore.Http;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Enum;

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
                .Value);
        }

        public User GetUser()
        {
            return _userRepositrory.GetFirstById(GetId());
        }
        
        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext!.User?.Identity?.IsAuthenticated ?? false; 
        }

        internal Role GetRole()
        {
            var httpContext = _contextAccessor.HttpContext;
            return (Role)int.Parse(httpContext
                .User
                .Claims
                .First(x => x.Type == "Role")
                .Value);
        }
    }
}
