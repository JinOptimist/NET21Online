namespace WebPortal.DbStuff.Models.Marketplace
{
    public class Smartphone : Product
    {
        public string ProductType { get; set; } = "Smartphone";
        public string OS { get; set; }
        public string OSVersion { get; set; }
        public double ScreenSize { get; set; }
        public string Resolution { get; set; }
        public int BatteryCapacity { get; set; }
        public bool Has5G { get; set; }
        public int CameraCount { get; set; }
    }
}