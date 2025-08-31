using WebPortal.DbStuff.Models;

namespace WebPortal.Services.Permissions
{
    public interface IMarketplacePermissions
    {
        bool CanDelete(Product product);
        bool CanAdd();
    }
}
