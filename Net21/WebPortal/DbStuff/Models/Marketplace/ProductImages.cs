namespace WebPortal.DbStuff.Models.Marketplace
{
    public class ProductImages : BaseModel
    {
        public string ImageUrl { get; set; }
        public bool IsMain { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}