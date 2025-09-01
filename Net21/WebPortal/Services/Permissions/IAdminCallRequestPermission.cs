using WebPortal.DbStuff.Models;

namespace WebPortal.Services.Permissions;

public interface IAdminCallRequestPermission
{
    bool CanDelete(CallRequest callRequest);
}