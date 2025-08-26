using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Models.Tourism;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories
{
    public class TourPreviewRepository : BaseRepository<TourPreview>, ITourPreviewRepository
    {

        public TourPreviewRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }

        public List<TourPreview> GetPopularListTitles()
        {
            return _portalContext.
                Tourisms.OrderBy(x => x.TourRating).
                Take(5).
                ToList();
        }

    }
}
