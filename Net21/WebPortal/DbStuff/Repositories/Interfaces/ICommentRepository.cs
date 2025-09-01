using WebPortal.DbStuff.Models;

namespace WebPortal.DbStuff.Repositories.Interfaces
{
    public interface ICommentRepository: IBaseRepository<CommentEntity>
    {
        List<CommentEntity> GetByGuitarId(int id);
    }
}
