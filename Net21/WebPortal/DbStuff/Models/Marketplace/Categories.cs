using WebPortal.DbStuff.Models.Marketplace.BaseItem;

namespace WebPortal.DbStuff.Models.Marketplace
{
    public class Categories : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}
