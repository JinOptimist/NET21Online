using static BullsAndCows.InvalidInputException;
using static BullsAndCows.Validation;

namespace BullsAndCows
{
    /// <summary>
    /// Human set secret. Human try to get answer
    /// </summary>
    internal class BullAndCowHumanVsHuman : BullAndCowBase
    {
        protected override string GenerateSecret()
        {
            Console.WriteLine("Set secret for Bulls and Cows");
            var answer = Console.ReadLine();
            Console.Clear();
            return answer;
        }

        protected override string GetGuess()
        {
            while (true) 
            {
                Console.WriteLine("Guess my number. Enter 4 diff number");
                var answer = Console.ReadLine(); // Читаем ввод

                try
                {
                    if (IsValid(answer))
                    {
                        return answer;
                    }

                    throw new InvalidInputException("Error: Enter 4 unique digits from 0 to 9.");
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        protected override void GetResponse((int bull, int cow) bullAndCow)
        {
            Console.WriteLine($"bull: {bullAndCow.bull} cow: {bullAndCow.cow}");
        }
    }
}
