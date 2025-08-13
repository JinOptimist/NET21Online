using WebPortal.DbStuff.Models.CompShop;
using WebPortal.DbStuff.Models.CompShop.Devices;

namespace WebPortal.Models.CompShop
{
    public class StartPageViewModel
    {
        public List<List<BaseDevice>> DevicesOfThree { get; set; }

        public List<News> News { get; set; }
    }
}
