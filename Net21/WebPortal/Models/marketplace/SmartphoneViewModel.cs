namespace WebPortal.Models.Marketplace
{
    public class SmartphoneViewModel : ProductViewModel
    {
        public string OS { get; set; }
        public double ScreenSize { get; set; }
        public int BatteryCapacity { get; set; }
        public bool Has5G { get; set; }
    }
}