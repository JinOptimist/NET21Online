
namespace WebPortal.Services
{
    public interface ICoffeShopFileServices
    {
        void UploudFonCoffeShop(IFormFile file);
        List<string> GetFonGallery();

    }
}