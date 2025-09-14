namespace WebPortal.Services;

public interface IAuthService
{
    bool IsAuthenticated();
    int GetId();
}