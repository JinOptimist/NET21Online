using WebPortal.DbStuff.Models;

namespace WebPortal.DbStuff.Repositories.Interfaces;

public interface ICdekChatRepository : IBaseRepository<CdekChat>
{
    CdekChat GetByIdWithUsers(int cdekChatId);
    List<CdekChat> GetNewMessagesForMe(int userId);
}