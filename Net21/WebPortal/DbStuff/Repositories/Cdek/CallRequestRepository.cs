using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models.Cdek;

namespace WebPortal.DbStuff.Repositories.Cdek
{
    public class CallRequestRepository : ICallRequestRepository
    {
        private readonly WebPortalContext _context;

        public CallRequestRepository(WebPortalContext context)
        {
            _context = context;
        }

        public IEnumerable<CallRequest> GetAll()
        {
            return _context.CallRequests.ToList();
        }

        public void Add(CallRequestViewModel request)
        {
            _context.CallRequests.Add(new CallRequest
            {
                Name = request.Name,
                Question = request.Question,
                PhoneNumber = request.PhoneNumber,
                Status = "Новая",
                CreatedAt = DateTime.Now
            });
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            var request = _context.CallRequests.Find(id);
            if (request != null)
            {
                _context.CallRequests.Remove(request);
                _context.SaveChanges();
            }
        }
    }
}