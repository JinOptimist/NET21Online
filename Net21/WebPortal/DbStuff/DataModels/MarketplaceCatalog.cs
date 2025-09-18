namespace WebPortal.DbStuff.DataModels
{
    public class MarketplaceCatalog
    {
        public int Id { get; set; }
        public string ProductType { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string CategoryName { get; set; }
        public string OwnerName { get; set; }
    }
}
