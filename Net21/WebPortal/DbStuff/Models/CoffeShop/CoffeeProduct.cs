namespace WebPortal.DbStuff.Models.CoffeShop
{
    public class CoffeeProduct : BaseModel
    {
        public string Img { get; set; }
        public string Name { get; set; }
        public int Cell { get; set; }
        public int AuthorId { get; set; }
        //public virtual UserCoffeShop AuthorAdd { get; set; }
        public virtual User AuthorAdd { get; set; }
    
    
    }
}
