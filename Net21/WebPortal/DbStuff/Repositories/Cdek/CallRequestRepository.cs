using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models.Cdek;

namespace WebPortal.DbStuff.Repositories.Cdek
{
    public class CallRequestRepository : BaseRepository<CallRequest>, ICallRequestRepository
    {
        public CallRequestRepository(WebPortalContext portalContext) : base(portalContext)
        {
            _portalContext = portalContext;
        }

        public IEnumerable<CallRequest> GetAll()
        {
            return _portalContext.CallRequests.ToList();
        }

        public void Add(CallRequestViewModel request)
        {
            _portalContext.CallRequests.Add(new CallRequest
            {
                Name = request.Name,
                Question = request.Question,
                PhoneNumber = request.PhoneNumber,
                Status = "Новая",
                CreatedAt = DateTime.Now
            });
            _portalContext.SaveChanges();
        }

        public void Remove(int id)
        {
            var request = _portalContext.CallRequests.Find(id);
            if (request != null)
            {
                _portalContext.CallRequests.Remove(request);
                _portalContext.SaveChanges();
            }
        }
    }
}