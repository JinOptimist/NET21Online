namespace WebPortal.DbStuff.Models.Marketplace
{
    public class Categories : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Product> Products { get; set; } = new();
    }
}
