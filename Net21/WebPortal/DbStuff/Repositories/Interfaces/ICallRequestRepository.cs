using WebPortal.DbStuff.Models;

namespace WebPortal.DbStuff.Repositories.Interfaces;

public interface ICallRequestRepository 
{
    Task<CallRequest> AddAsync(CallRequest entity, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    Task<List<CallRequest>> GetAllAsync(CancellationToken ct = default);
    Task<CallRequest?> GetByIdAsync(int id, CancellationToken ct = default);
}