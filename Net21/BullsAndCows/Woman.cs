namespace BullsAndCows
{
    public class Woman : Human
    {
        public string FavoriteColor { get; set; }

        public Woman(string favoriteColor) : base(DateTime.Now.Year - 18)
        {
            FavoriteColor = favoriteColor;
        }

        public Woman(string name, int yearOfBirth, string favoriteColor) : base(name, yearOfBirth)
        {
            FavoriteColor = favoriteColor;
        }

        public override void SpeakAboutMySelf()
        {
            Console.WriteLine($"I'm {Name}. I'm prety yang. I like {FavoriteColor}");
        }
    }
}
