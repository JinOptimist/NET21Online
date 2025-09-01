using WebPortal.DbStuff.Models;
using WebPortal.Enum;

namespace WebPortal.Services.Permissions;

public class AdminCallRequestPermission : IAdminCallRequestPermission
{
    private AuthService _authService;
    
    public AdminCallRequestPermission(AuthService authService)
    {
        _authService = authService;
    }

    public bool CanDelete(CallRequest request)
    {
        if (!_authService.IsAuthenticated())
        {
            return false;
        }
        
        var user = _authService.GetUser();
        if (user.Role == Role.Admin)
        {
            return true;
        }
        // HELP что возвращать, если удалять и видеть кнопочку может администратор?
        return request.Author?.Id == user.Id;
    }
}