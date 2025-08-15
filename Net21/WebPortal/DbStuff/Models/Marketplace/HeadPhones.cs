namespace WebPortal.DbStuff.Models.Marketplace
{
    public class HeadPhones
    {
        public string ConnectionType { get; set; } // Wired/Wireless
        public bool HasNoiseCancellation { get; set; }
        public string Impedance { get; set; } // Ohms
        public string FrequencyResponse { get; set; }
        public bool HasMicrophone { get; set; }
        public int BatteryLife { get; set; } // hours (для беспроводных)
    }
}
