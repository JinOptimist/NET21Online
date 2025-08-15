using WebPortal.Models.Marketplace;

namespace WebPortal.Models.marketplace
{
    public class SmartWatchViewModel : ProductBase
    {
        public bool HasHeartRateMonitor { get; set; }
        public bool IsWaterResistant { get; set; }
        public string CompatibleOS { get; set; }
        public int BatteryLife { get; set; } // in days
    }
}
