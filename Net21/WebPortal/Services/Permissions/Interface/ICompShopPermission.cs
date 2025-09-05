namespace WebPortal.Services.Permissions.Interface
{
    public interface ICompShopPermission
    {
        bool CanDelete();
        string GetNameUser();
    }
}