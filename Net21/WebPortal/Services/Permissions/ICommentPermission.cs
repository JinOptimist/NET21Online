using WebPortal.DbStuff.Models;

namespace WebPortal.Services.Permissions
{
    public interface ICommentPermission
    {
        bool CanDelete(CommentEntity comment);
    }
}