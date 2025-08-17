using WebPortal.DbStuff.Models;
namespace WebPortal.DbStuff.Repositories;

public class CallRequestRepository : BaseRepository<CallRequest>
{
    private readonly WebPortalContext _context;

    public CallRequestRepository(WebPortalContext portalContext) : base(portalContext)
    {
    }
    
    public IOrderedEnumerable<CallRequest> GetList()
    {
       return _context.CallRequests
           .ToList()
           .OrderBy(x => x.CreationTime);
    }
}