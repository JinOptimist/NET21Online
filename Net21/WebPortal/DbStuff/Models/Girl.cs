namespace WebPortal.DbStuff.Models
{
    public class Girl : BaseModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int Age { get; set; }
        public int? Size { get; set; }
    }
}
