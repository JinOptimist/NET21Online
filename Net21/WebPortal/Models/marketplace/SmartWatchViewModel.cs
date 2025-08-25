using WebPortal.Models.marketplace.BaseViewModel;

namespace WebPortal.Models.Marketplace
{
    public class SmartWatchViewModel : ProductViewModel
    {
        public bool HasHeartRateMonitor { get; set; }
        public bool IsWaterResistant { get; set; }
        public string CompatibleOS { get; set; }
        public int BatteryLife { get; set; }
    }
}