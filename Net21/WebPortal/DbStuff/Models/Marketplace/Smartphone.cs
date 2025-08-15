using WebPortal.DbStuff.Models.Marketplace.BaseItem;

namespace WebPortal.DbStuff.Models.Marketplace
{
    public class Smartphone : Product
    {
        public string OS { get; set; }
        public string OSVersion { get; set; }
        public double ScreenSize { get; set; } // inches
        public string Resolution { get; set; }
        public int BatteryCapacity { get; set; } // mAh
        public bool Has5G { get; set; }
        public int CameraCount { get; set; }
    }
}
