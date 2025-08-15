using WebPortal.Models.Marketplace;

namespace WebPortal.Models.marketplace
{
    public class SmarphoneViewModel : ProductBase
    {
        public string OS { get; set; }
        public double ScreenSize { get; set; }  
        public int BatteryCapacity { get; set; } 
        public bool Has5G { get; set; }
    }
}
