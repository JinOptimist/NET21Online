namespace BullsAndCows
{
    public class Human
    {
        public string Adress {  get; set; }

        public string Name { get; set; }

        private int _yearOfBirth;

        private int Age => DateTime.Now.Year - _yearOfBirth;

        public bool IsAdult => Age > 18;
        //public bool IsAdult
        //{
        //    get
        //    {
        //        return Age > 18;
        //    }
        //}

        public Human(int yearOfBirth)
        {
            _yearOfBirth = yearOfBirth;
        }

        public Human(string name, int yearOfBirth)
        {
            Name = name;
            _yearOfBirth = yearOfBirth;
        }

        public virtual void SpeakAboutMySelf()
        {
            Console.WriteLine($"I'm {Name}. I'm {Age}");
        }
    }
}
