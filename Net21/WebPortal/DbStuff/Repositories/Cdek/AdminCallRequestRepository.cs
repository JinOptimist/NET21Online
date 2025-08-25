using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces.Notes;

namespace WebPortal.DbStuff.Repositories.Cdek;

public class AdminCallRequestRepository : IAdminCallRequestRepository
{
    private readonly WebPortalContext _context;

    public AdminCallRequestRepository(WebPortalContext context)
    {
        _context = context;
    }
    
    public IEnumerable<CallRequest> GetAll()
    {
        return _context.CallRequests.OrderByDescending(r => r.CreatedAt).ToList();
    }

    public CallRequest GetById(int id)
    {
        return _context.CallRequests.Find(id);
    }

    public void Update(CallRequest request)
    {
        _context.CallRequests.Update(request);
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