using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories;

public class CallRequestRepository : ICallRequestRepository
{
    private readonly WebPortalContext _context;

    public CallRequestRepository(WebPortalContext context)
    {
        _context = context;
    }

    // Добавить заявку в базу
    public async Task<CallRequest> AddAsync(CallRequest entity, CancellationToken ct = default)
    {
        await _context.CallRequests.AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);
        return entity;
    }

    // Удалить заявку по Id
    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await _context.CallRequests.FindAsync(new object[] { id }, ct);
        if (entity == null) return false;

        _context.CallRequests.Remove(entity);
        await _context.SaveChangesAsync(ct);
        return true;
    }

    // Получить список всех заявок
    public async Task<List<CallRequest>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.CallRequests
            .OrderByDescending(x => x.CreationTime)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    // Получить одну заявку по Id
    public async Task<CallRequest?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.CallRequests
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }
}