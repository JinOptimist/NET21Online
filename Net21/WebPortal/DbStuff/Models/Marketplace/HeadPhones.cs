using WebPortal.DbStuff.Models.Marketplace.BaseItem;

namespace WebPortal.DbStuff.Models.Marketplace
{
    public class Headphones : Product
    {
        public string ProductType { get; set; } = "Headphones";
        public string ConnectionType { get; set; }
        public bool HasNoiseCancellation { get; set; }
        public string Impedance { get; set; } 
        public string FrequencyResponse { get; set; }
        public bool HasMicrophone { get; set; }
        public int BatteryLife { get; set; }
    }
}
