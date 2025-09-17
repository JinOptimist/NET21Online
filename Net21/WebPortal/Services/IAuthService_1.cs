using WebPortal.DbStuff.Models;
using WebPortal.Enum;

namespace WebPortal.Services;

public interface IAuthService
{
    int GetId();
    User GetUser();
    bool IsAuthenticated();
    Language GetLanguage();
    Role GetRole();
    string GetName();
    bool IsAdmin();
}