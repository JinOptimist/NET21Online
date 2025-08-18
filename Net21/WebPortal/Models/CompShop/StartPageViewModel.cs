using WebPortal.DbStuff.Models.CompShop;
using WebPortal.DbStuff.Models.CompShop.Devices;
using WebPortal.Models.CompShop.Device;

namespace WebPortal.Models.CompShop
{
    public class StartPageViewModel
    {
        public List<List<DeviceViewModel>> DevicesOfThree { get; set; }

        public List<NewsViewModel> News { get; set; }
    }
}
