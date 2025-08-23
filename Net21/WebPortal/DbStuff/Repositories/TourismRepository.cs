using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Models.Tourism;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories
{
    public class TourismRepository : BaseRepository<Tourism>, ITourismRepository
    {

        public TourismRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }

        public List<Tourism> GetPopularListTitles()
        {
            return _portalContext.
                Tourisms.OrderBy(x => x.TitleRating).
                Take(5).
                ToList();
        }

    }
}
