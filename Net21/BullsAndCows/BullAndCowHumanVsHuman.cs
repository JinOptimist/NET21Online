using System.Net.Sockets;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BullsAndCows
{
    /// <summary>
    /// Human set secret. Human try to get answer
    /// </summary>
    internal class BullAndCowHumanVsHuman : BullAndCowBase
    {
        protected override string GenerateSecret()
        {
            string secret = GetNumberFromUser("Please Enter the 4 different characters");
            Console.Clear();
            return secret;
        }

        protected override string GetGuess()
        {
            string guess = GetNumberFromUser("Guess the number. Enter 4 different digits:");
            return guess;
        }

        protected override void GetResponse((int bull, int cow) bullAndCow)
        {
            Console.WriteLine($"bull: {bullAndCow.bull} cow: {bullAndCow.cow}");
        }
        public string GetNumberFromUser(string message)
        {

            int value;
            bool isValidValue;
            string inputValue;
            do
            {
                Console.WriteLine(message);
                inputValue = Console.ReadLine();
                var isItNumber = int.TryParse(inputValue, out value);
                isValidValue = inputValue.Length < 4 || !isItNumber || inputValue.Length > 4;

            }
            while (isValidValue);
            return inputValue;
        }
    }
}
