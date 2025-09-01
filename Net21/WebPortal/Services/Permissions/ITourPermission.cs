using WebPortal.DbStuff.Models.Tourism;

namespace WebPortal.Services.Permissions
{
    public interface ITourPermission
    {
        bool CanDelete(Tours tour);
    }
}