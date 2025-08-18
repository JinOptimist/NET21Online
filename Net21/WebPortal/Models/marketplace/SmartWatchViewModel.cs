namespace WebPortal.Models.Marketplace
{
    public class SmartWatchViewModel : ProductBaseViewModel
    {
        public bool HasHeartRateMonitor { get; set; }
        public bool IsWaterResistant { get; set; }
        public string CompatibleOS { get; set; }
        public int BatteryLife { get; set; }
    }
}
