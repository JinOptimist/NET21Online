using WebPortal.DbStuff.Models;
namespace WebPortal.DbStuff.Repositories;

public class CdekCallRequestRepository
{
    private readonly WebPortalContext _context;
    public CdekCallRequestRepository(WebPortalContext context) 
        => _context = context;

    public void Add(CdekCallRequest request)
    {
        _context.CdekCallRequests.Add(request);
        _context.SaveChanges();
    }

    public List<CdekCallRequest> GetAll() 
        => _context.CdekCallRequests.ToList();
}