using WebPortal.DbStuff.Models;

namespace WebPortal.DbStuff.Repositories.Interfaces.Notes;

public interface IAdminCallRequestRepository
{
    IEnumerable<CallRequest> GetAll();
    CallRequest GetById(int id);
    void Update(CallRequest request);
    void Remove(int id);
}