using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models.Cdek;
using WebPortal.Services.Permissions;

namespace WebPortal.DbStuff.Repositories.Cdek;

public class AdminCallRequestRepository : BaseRepository<CallRequest>, IAdminCallRequestRepository
{
    private AdminCallRequestPermission _adminCallRequestPermission;

    public AdminCallRequestRepository(WebPortalContext portalContext) : base(portalContext)
    {
        _portalContext = portalContext;
    }

    /// <summary>
    /// Список всех заявок с фильтрацией
    /// </summary>
    /// <param name="search"></param>
    /// <param name="statusFilter"></param>
    /// <returns></returns>
    public IEnumerable<CallRequest> GetFilteredRequests(string search = "", string statusFilter = "")
    {
        var requests = _portalContext.CallRequests
            .Select(r => new CallRequest
            {
                Id = r.Id,
                Name = r.Name,
                PhoneNumber = r.PhoneNumber,
                Question = r.Question,
                Status = r.Status,
                CreatedAt = r.CreatedAt,
            });

        if (!string.IsNullOrEmpty(search))
        {
            requests = requests.Where(r =>
                r.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                r.PhoneNumber.Contains(search));
        }

        if (!string.IsNullOrEmpty(statusFilter))
        {
            requests = requests.Where(r => r.Status == statusFilter);
        }

        return requests.OrderByDescending(r => r.CreatedAt).ToList();
    }
    
    public IEnumerable<CallRequest> GetAll()
    {
        return _portalContext.CallRequests.OrderByDescending(r => r.CreatedAt).ToList();
    }

    public CallRequest GetById(int id)
    {
        return _portalContext.CallRequests.Find(id);
    }

    public void Update(CallRequest request)
    {
        _portalContext.CallRequests.Update(request);
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