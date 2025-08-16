using WebPortal.DbStuff.Models.CompShop;
using WebPortal.Models.CompShop.Device;

namespace WebPortal.Models.CompShop
{
    public class AddPageViewModel
    {
        public DeviceViewModel? DeviceViewModel { get; set; }

        public List<Category> Categoryes { get; set; }

        public List<TypeDeviceViewModel> TypeDevices { get; set; }
    }
}
