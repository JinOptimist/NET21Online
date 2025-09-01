using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories
{
    public class CommentRepository: BaseRepository<CommentEntity>, ICommentRepository
    {
        public CommentRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }

        public List<CommentEntity> GetByGuitarId(int id)
        {
            return _dbSet.Include(c => c.Author).Where(c => c.GuitarId == id).ToList();
        }
    }
}
