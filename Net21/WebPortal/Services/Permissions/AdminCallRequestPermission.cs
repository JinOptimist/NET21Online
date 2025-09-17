using WebPortal.DbStuff.Models;
using WebPortal.Enum;

namespace WebPortal.Services.Permissions;

public class AdminCallRequestPermission : IAdminCallRequestPermission
{
    private IAuthService _authService;
    
    public AdminCallRequestPermission(IAuthService authService)
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
        
        return false;
    }
}