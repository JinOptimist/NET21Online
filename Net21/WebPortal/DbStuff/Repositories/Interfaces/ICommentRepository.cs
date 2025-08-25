using WebPortal.DbStuff.Models;

namespace WebPortal.DbStuff.Repositories.Interfaces
{
    public interface ICommentRepository: IBaseRepository<CommentEntity>
    {
        CommentEntity? GetByMessage(string Message);
    }
}
