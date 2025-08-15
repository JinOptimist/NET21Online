namespace WebPortal.DbStuff.Models.Marketplace.BaseItem
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        // Связи
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string SellerId { get; set; }  // Идентификатор пользователя
        public List<ProductImage> Images { get; set; } = new();
    }
}
