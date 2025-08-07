namespace WebPortal.Models.UnderTheBridge;

public class GuitarViewModel
{
    public GuitarViewModel(string name, float mark, int reviewAmount, decimal price, AccessStatus status, string image)
    {
        Name = name;
        Mark = mark;
        ReviewAmount = reviewAmount;
        Price = price;
        Status = status;
        Image = image;
    }

    public GuitarViewModel()
    {
        
    }
    
    public string? Name { get; set; }
    public float? Mark { get; set; }
    public int? ReviewAmount { get; set; }
    public decimal? Price { get; set; }
    public AccessStatus? Status { get; set; }
    public string? Image { get; set; }
}

public enum AccessStatus
{
    InShop,
    InStock,
    No
}