using WebPortal.Models.CompShop.Devices;

namespace WebPortal.Models.CompShop
{
    public class AddPageViewModel
    {
        public DeviceViewModel? DeviceViewModel { get; set; } = new DeviceViewModel();

        public List<Category> Categoryes { get; set; }

        public List<TypeDevice> TypeDevices { get; set; }
    }
}
