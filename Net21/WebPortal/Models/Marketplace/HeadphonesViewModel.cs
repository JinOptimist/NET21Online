using WebPortal.Models.Marketplace;

namespace WebPortal.Models.marketplace
{
    public class HeadphonesViewModel : ProductBase
    {
        public string ConnectionType { get; set; } // "Wired", "Wireless", "Both"
        public bool HasNoiseCancellation { get; set; }
        public string Impedance { get; set; }
    }
}
