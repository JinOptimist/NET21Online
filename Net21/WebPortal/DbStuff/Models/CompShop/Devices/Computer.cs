namespace WebPortal.DbStuff.Models.CompShop.Devices
{
    public class Computer : BaseModel
    {
        public string Processor { get; set; }

        public int Ram { get; set; }

        public double Memory { get; set; }

        public string VideoCard { get; set; } 
        
        public string Motherboard { get; set; }

        public int PowerUnit { get; set; }

        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }
    }
}