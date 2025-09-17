namespace WebPortal.Services;

public interface ICdekService
{
    int GetUserId();
    bool IsAuthenticated();
}