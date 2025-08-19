using WebPortal.DbStuff.Models.Marketplace.BaseItem;

namespace WebPortal.DbStuff.Models.Marketplace
{
    public class Laptop : Product
    {
        public string Processor { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }
        public string StorageType { get; set; }
        public string OS { get; set; }
        public double ScreenSize { get; set; }
        public string GraphicsCard { get; set; }
    }
}