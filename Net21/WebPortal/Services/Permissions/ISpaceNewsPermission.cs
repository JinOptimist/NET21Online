using WebPortal.DbStuff.Models;

namespace WebPortal.Services.Permissions
{
    public interface ISpaceNewsPermission
    {
        bool CanRemove(SpaceNews spaceNews);
    }
}