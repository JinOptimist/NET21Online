namespace WebPortal.DbStuff.Models.CompShop.Devices
{
    public class Computer : BaseDevice
    {
        public string Processor { get; set; }

        public int Ram { get; set; }

        public double Memory { get; set; }

        public string VideoCard { get; set; } 
        
        public string Motherboard { get; set; }

        public int PowerUnit { get; set; }
    }
}