using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models;
using WebPortal.Models.Cdek;

namespace WebPortal.DbStuff.Repositories.Interfaces;

public interface ICallRequestRepository
{
    IEnumerable<CallRequest> GetAll();
    void Add(CallRequestViewModel request);
    void Remove(int id);
}