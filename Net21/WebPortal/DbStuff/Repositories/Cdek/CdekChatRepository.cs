using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories.Cdek
{
    public class CdekChatRepository : BaseRepository<CdekChat>, ICdekChatRepository
    {
        public CdekChatRepository(WebPortalContext portalContext) : base(portalContext) { }

        public CdekChat GetByIdWithUsers(int cdekChatId)
        {
            return _dbSet
                .Include(x => x.UserWhoViewedIt)
                .Include(x => x.Author)
                .First(x => x.Id == cdekChatId);
        }

        public List<CdekChat> GetNewMessagesForMe(int userId)
        {
            var lastWeek = DateTime.Now.AddDays(-7);
            return _dbSet
                .Include(x => x.Author)
                .Where(m =>
                    !m.UserWhoViewedIt.Select(u => u.Id).Contains(userId)
                    && m.CreatedAt > lastWeek)
                .ToList();
        }
    }
}