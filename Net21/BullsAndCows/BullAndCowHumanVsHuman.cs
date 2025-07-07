

namespace BullsAndCows
{
    /// <summary>
    /// Human set secret. Human try to get answer
    /// </summary>
    internal class BullAndCowHumanVsHuman : BullAndCowBase
    {
        protected override string GenerateSecret()

        {
            static string GetUniqueFourDigitNumberAsString()
            {
                while (true)
                {
                    Console.Write("Please enter a four digit number: ");
                    string input = Console.ReadLine();

                    if (input.Length != 4 || !int.TryParse(input, out int number) || number < 1000 || number > 9999)
                    {
                        Console.WriteLine("Error: number must be four digits ");
                        continue;
                    }

                    int digit1 = number / 1000;
                    int digit2 = (number / 100) % 10;
                    int digit3 = (number / 10) % 10;
                    int digit4 = number % 10;

                    if (digit1 != digit2 && digit1 != digit3 && digit1 != digit4 &&
                        digit2 != digit3 && digit2 != digit4 &&
                        digit3 != digit4)
                    {
                        return input;
                    }
                    else
                    {
                        Console.WriteLine("Error: numbers must be different ");
                    }
                }
            }


            string answer = GetUniqueFourDigitNumberAsString();


            Console.Clear();

            return answer;
        }

        protected override string GetGuess()
        {
            Console.WriteLine("Guess my number. Enter 4 diff number");
            var answer = Console.ReadLine();

            while (answer == null || answer.Length != 4 || !answer.All(char.IsDigit))
            {
                Console.WriteLine("Please enter exactly 4 digits. Try again:");
                answer = Console.ReadLine();
            }
            return answer;
        }

        protected override void GetResponse((int bull, int cow) bullAndCow)
        {
            Console.WriteLine($"bull: {bullAndCow.bull} cow: {bullAndCow.cow}");
        }
    }
}
