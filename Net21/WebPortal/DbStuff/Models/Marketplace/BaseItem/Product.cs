using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Models.Marketplace;

public class Product : BaseModel
{
    public string ProductType { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
    public int? CategoryId { get; set; }
    public virtual Categories Category { get; set; }
    public int? OwnerId { get; set; }
    public virtual User User { get; set; }
    public virtual List<ProductImages> Images { get; set; } = new();
}