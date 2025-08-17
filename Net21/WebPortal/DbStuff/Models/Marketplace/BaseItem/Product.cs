namespace WebPortal.DbStuff.Models.Marketplace.BaseItem
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductType { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public int? CategoryId { get; set; }
        public Categories Category { get; set; }
        public List<ProductImages> Images { get; set; } = new();
    }
}