namespace WebPortal.DbStuff.Models.Marketplace
{
    public class SmartWatch : Product
    {
        public string ProductType { get; set; } = "SmartWatch";
        public bool HasHeartRateMonitor { get; set; }
        public bool HasBloodOxygenMonitor { get; set; }
        public bool IsWaterResistant { get; set; }
        public string WaterResistanceLevel { get; set; }
        public string CompatibleOS { get; set; }
        public int BatteryLife { get; set; }
    }
}