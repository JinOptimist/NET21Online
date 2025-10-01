using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Enum;
using WebPortal.Services.AutoRegistrationInDI;

namespace WebPortal.Services
{
    [AutoResolve]
    public class AuthService : IAuthService, ILanguageService
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

        public Language GetLanguage()
        {
            return (Language)int.Parse(_contextAccessor.HttpContext
                .User
                .Claims
                .First(x => x.Type == "Language")
                .Value);
        }

        public Role GetRole()
        {
            var httpContext = _contextAccessor.HttpContext;
            return (Role)int.Parse(httpContext
                .User
                .Claims
                .First(x => x.Type == "Role")
                .Value);
        }

        public string GetName()
        {
            var httpContext = _contextAccessor.HttpContext;
            return httpContext
                .User
                .Claims
                .First(x => x.Type == "Name")
                .Value;
        }

        public bool IsAdmin()
        {
            return IsAuthenticated() 
                ? GetRole() == Role.Admin 
                : false;
        }
    }
}
