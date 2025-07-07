namespace BullsAndCows
{
    /// <summary>
    /// Human set secret. Human try to get answer
    /// </summary>
    internal class BullAndCowHumanVsHuman : BullAndCowBase
    {
        protected override string GenerateSecret()
        {
            Console.WriteLine("Enter a number of 4 characters");
            var Human1 = Console.ReadLine();

            return Human1;
        }


        protected override string GetGuess() // нвдо ввести 4 числа 
        {
            Console.WriteLine("Guess my number. Enter 4 different digits ");
            var Human1 = Console.ReadLine();
            return Human1;
        }

        protected override void GetResponse((int bull, int cow) bullAndCow) // вывод
        {
            Console.WriteLine($"bull: {bullAndCow.bull} cow: {bullAndCow.cow}");
        }
    }
}
