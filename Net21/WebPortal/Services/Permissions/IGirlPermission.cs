using WebPortal.DbStuff.Models;

namespace WebPortal.Services.Permissions
{
    public interface IGirlPermission
    {
        bool CanDelete(Girl girl);
    }
}