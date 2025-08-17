using WebPortal.DbStuff.Models;
namespace WebPortal.DbStuff.Repositories;

public class CdekCallRequestRepository
{
    private readonly WebPortalContext _context;

    public CdekCallRequestRepository(WebPortalContext context)
    {
        _context = context;
    }

    public List<CallRequest> GetList()
    {
       return _context.CallRequests.ToList();
    }
}