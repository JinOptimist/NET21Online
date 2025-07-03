using System;
using System.Net.Sockets;

namespace BullsAndCows
{
    /// <summary>
    /// Human set secret. Bot try to get answer
    /// </summary>
    public class BullAndCowBotVsHuman : BullAndCowBase
    {
        private List<string> _possibleNumbers;
        private string _gueus;

        protected override string GenerateSecret()
        {
            var secret = "";

            do
            {
                Console.WriteLine("Write a 4-digit number with different digits [1234]");
                secret = Console.ReadLine();
            }
            while (!ValidateFourDigitNumber(secret));

            Console.Clear();
            Console.WriteLine($"Your number is {secret}");
            Console.WriteLine();

            GenerateAllPossibleNumbers();

            return secret;
        }

        protected override void GetAnswer((int bull, int cow) bullAndCow)
        {
            var bulls = "";
            var cows = "";

            bool validate_bulls = false;
            bool validate_cows = false;

            do
            {
                Console.Write("Bulls: ");
                bulls = Console.ReadLine();

                validate_bulls = !ValidateDigit(bulls);

                Console.Write("Cows: ");
                cows = Console.ReadLine();

                validate_cows = !ValidateDigit(cows);
            }
            while (validate_bulls && validate_cows);

            UpdatePossibleNumbers(_gueus, int.Parse(bulls), int.Parse(cows));
        }

        private void UpdatePossibleNumbers(string guess, int bulls, int cows)
        {
            _possibleNumbers = _possibleNumbers
                .Where(candidate => CalculateBullAndCowForBot(candidate, guess) == (bulls, cows))
                .ToList();
        }

        private (int bull, int cow) CalculateBullAndCowForBot(string candidate, string guess)
        {
            int bull = 0;
            int cow = 0;

            for (int i = 0; i < 4; i++)
            {
                if (guess[i] == candidate[i])
                {
                    bull++;
                }
            }

            int commonDigits = guess.Distinct().Intersect(candidate.Distinct()).Count();
            cow = commonDigits - bull;

            return (bull, cow);
        }

        private bool ValidateDigit(string digit)
        {
            if (string.IsNullOrWhiteSpace(digit))
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("Input is empty. Please enter a digit from 0 to 9.");
                Console.WriteLine();
                return false;
            }

            if (digit.Length != 1)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("Input must be exactly 1 characters long.");
                Console.WriteLine();
                return false;
            }

            if (!int.TryParse(digit, out _))
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("Please enter a valid 1-digit number (from 0 to 9 only).");
                Console.WriteLine();
                return false;
            }

            return true;
        }

        protected override string GetGuess()
        {
            var random = new Random();

            if (_possibleNumbers.Count == 0)
            {
                throw new InvalidOperationException("Нет подходящих чисел!");
            }

            _gueus = _possibleNumbers[random.Next(_possibleNumbers.Count)];
            Console.WriteLine("Bot think, that your number is " + _gueus);
            return _gueus;
        }

        private void GenerateAllPossibleNumbers()
        {
            _possibleNumbers = new List<string>();

            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    for (int k = 0; k <= 9; k++)
                    {
                        for (int l = 0; l <= 9; l++)
                        {
                            if (i != j && i != k && i != l &&
                                j != k && j != l &&
                                k != l)
                            {
                                var number = $"{i}{j}{k}{l}";
                                _possibleNumbers.Add(number);
                            }
                        }
                    }
                }
            }
        }

        protected override void EndGame(string secret)
        {
            Console.Clear();
            Console.WriteLine($"Bot win. Number was {secret}");
        }
    }
}
