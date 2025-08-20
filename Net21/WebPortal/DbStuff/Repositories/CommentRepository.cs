using UnderTheBridge.Data.Models;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories
{
    public class CommentRepository: BaseRepository<CommentEntity>, ICommentRepository
    {
        public CommentRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }
    }
}
