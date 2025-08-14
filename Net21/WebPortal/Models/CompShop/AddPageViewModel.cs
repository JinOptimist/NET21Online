using WebPortal.DbStuff.Models.CompShop;
using WebPortal.DbStuff.Models.CompShop.Devices;

namespace WebPortal.Models.CompShop
{
    public class AddPageViewModel
    {
        public BaseDevice? DeviceViewModel { get; set; } = new BaseDevice();

        public List<Category> Categoryes { get; set; }

        public List<TypeDevice> TypeDevices { get; set; }
    }
}
