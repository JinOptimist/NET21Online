using WebPortal.Models.CompShop.Devices;

namespace WebPortal.Models.CompShop
{
    public class StartPageViewModel
    {
        public List<List<DeviceViewModel>> DevicesOfThree { get; set; }

        public List<NewsViewModel> News { get; set; }
    }
}
