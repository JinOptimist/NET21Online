namespace WebPortal.DbStuff.Models.CompShop.Devices
{
    public class Device : BaseModel
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int TypeDeviceId { get; set; }

        public int CategoryId { get; set; }

        public double? Price { get; set; }

        public string? Image { get; set; }

        public double? Rating { get; set; }

        public bool IsPopular { get; set; } 

        public CategoryEnum CategoryEnum { get; set; }

        public virtual TypeDevice TypeDevice { get; set; }
        public virtual Category Category { get; set; }
        public virtual Computer Computer { get; set; }
    }
}