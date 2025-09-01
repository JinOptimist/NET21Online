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

    public bool CanDelete(CallRequest callRequest)
    {
        if (!_authService.IsAuthenticated())
        {
            return false;
        }
        
        var user = _authService.GetUser();
        if (user.Role == Role.Admin
            || user.Role == Role.GrilModrator)
        {
            return true;
        }
        
        return callRequest.Author?.Id == user.Id;
    }
}