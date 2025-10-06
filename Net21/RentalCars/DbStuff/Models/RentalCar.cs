namespace RentalCarsMinimalApi.DBContext.Models
{
    public class RentalCar: BaseModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

    }
}
