using WebPortal.DbStuff.Models;
using WebPortal.Models.Cdek;

namespace WebPortal.DbStuff.Repositories.Interfaces;

public interface IAdminCallRequestRepository
{
    IEnumerable<CallRequest> GetFilteredRequests(string search = "", string statusFilter = "");
    IEnumerable<CallRequest> GetAll();
    CallRequest GetById(int id);
    void Update(CallRequest request);
    void Remove(int id);
}