namespace WebPortal.Models.Marketplace
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsAvailable { get; set; } = true;
        public string ProductType { get; set; }
        public bool IsActive { get; set; }
        public List<ProductViewModel> Products { get; set; } = new();
    }
}