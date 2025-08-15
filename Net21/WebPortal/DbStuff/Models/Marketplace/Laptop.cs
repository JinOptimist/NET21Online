using WebPortal.DbStuff.Models.Marketplace.BaseItem;

namespace WebPortal.DbStuff.Models.Marketplace
{
    public class Laptop : Product
    {
        public string Processor { get; set; }
        public int RAM { get; set; } // GB
        public int Storage { get; set; } // GB
        public string StorageType { get; set; } // SSD/HDD
        public string OS { get; set; }
        public double ScreenSize { get; set; } // inches
        public string GraphicsCard { get; set; }
    }
}
