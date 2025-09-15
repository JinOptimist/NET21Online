using Microsoft.EntityFrameworkCore;
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
    
    public List<AdminCdekStatusViewModel> GetStatistics()
    {
        FormattableString fs = @$"
        SELECT *
FROM (SELECT CASE
                 WHEN Status = '' THEN N'Без статуса'
                 ELSE Status
                 END  AS StatusDisplay,
             COUNT(*) AS StatusRequests
      FROM CallRequests
      WHERE Status IN (N'Новая', N'Обработана', '')
      GROUP BY CASE
                   WHEN Status = '' THEN N'Без статуса'
                   ELSE Status
                   END

      UNION ALL

      SELECT N'Всего' AS StatusDisplay, -- название строки
             COUNT(*) AS StatusRequests -- считаем общее количество всех заявок
      FROM CallRequests
      WHERE Status IN (N'Новая', N'Обработана', '')
      )t

ORDER BY
    CASE
        WHEN StatusDisplay = N'Новая' THEN 1        
        WHEN StatusDisplay = N'Обработана' THEN 2   
        WHEN StatusDisplay = N'Без статуса' THEN 3  
        WHEN StatusDisplay = N'Всего' THEN 4        
    END";

        var response = _portalContext.Database
            .SqlQuery<AdminCdekStatusViewModel>(fs) // напрямую маппим SQL результат в твою модель
            .ToList();

        return response;
    }
    
    // Через LINQ работает
    /*public (int Всего, int Новая, int Обработана, int ПустойСтатус) GetStatistics()
    {
        var all = _portalContext.CallRequests.ToList();
        return (
            Всего: all.Count,
            Новая: all.Count(r => r.Status == "Новая"),
            Обработана: all.Count(r => r.Status == "Обработана"),
            ПустойСтатус: all.Count(r => string.IsNullOrEmpty(r.Status))
        );
    }*/
}