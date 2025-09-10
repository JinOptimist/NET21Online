using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models.SpaceStation;

namespace WebPortal.DbStuff.Repositories
{
    public class SpaceStationRepository : BaseRepository<SpaceNews>, ISpaceStationRepository
    {
        public SpaceStationRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }
        public List<SpaceNews> FirstNews()
        {
            return _dbSet
                    .Include(x => x.Author)
                    .ToList();
        }
        public bool IsUniqTitle(string? title)
        {
            return !_dbSet.Any(x => x.Title == title);
        }
        public List<AuthorStatisticsViewModel> GetAuthorStatistics()
        {
            var sql = @"
        WITH LastPublications AS (
            SELECT 
                AuthorId,
                Title,
                DateAdded,
                ROW_NUMBER() OVER (PARTITION BY AuthorId ORDER BY DateAdded DESC) as rn
            FROM SpaceNews
            WHERE AuthorId IS NOT NULL
        )
        
        SELECT 
            u.Id as AuthorId,
            u.UserName as AuthorName,
            COUNT(sn.Id) as PublicationCount,
            MAX(sn.DateAdded) as LastPublicationDate,
            lp.Title as LastPublicationTitle
        FROM Users u
        INNER JOIN SpaceNews sn ON u.Id = sn.AuthorId
        LEFT JOIN LastPublications lp ON u.Id = lp.AuthorId AND lp.rn = 1
        GROUP BY u.Id, u.UserName, lp.Title
        ORDER BY PublicationCount DESC";

            return _portalContext.Database
                .SqlQueryRaw<AuthorStatisticsViewModel>(sql)
                .AsNoTracking()
                .ToList();
        }
    }

}
