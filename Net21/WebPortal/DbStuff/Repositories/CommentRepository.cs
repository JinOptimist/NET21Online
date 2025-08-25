using Microsoft.Identity.Client;
using UnderTheBridge.Data.Models;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories
{
    public class CommentRepository: BaseRepository<CommentEntity>, ICommentRepository
    {
        public CommentRepository(WebPortalContext portalContext) : base(portalContext)
        {
            
        }

        public CommentEntity? GetByMessage(string Message)
        {
            return _dbSet.FirstOrDefault(x => x.Message == Message);
        }
    }
}
