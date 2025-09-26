namespace WebPortal.Hubs.Interfaces;

public interface ICdekChatClientHub
{
    Task ReceiveMessage(int id, string userName, string message);

}