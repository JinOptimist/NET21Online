namespace BullsAndCows
{
    /// <summary>
    /// Human set secret. Human try to get answer
    /// </summary>
    internal class BullAndCowHumanVsHuman : BullAndCowBase
    {
        public void Play()
        {
            Console.WriteLine("=== Human vs Human Mode ===");

            var secrete1 = ReadValidNumber("Player 1", "enter your secret number (4 unique digits)");
            Console.Clear();

            var secrete2 = ReadValidNumber("Player 2", "enter your secret number (4 unique digits)");
            Console.Clear();

            bool gameEnded = false;
            while (!gameEnded)
            {
                var guess1 = ReadValidNumber("Player 1", "make your guess");
                var result1 = Compare(secrete2, guess1);
                Console.WriteLine($"Player 1 -> Bulls: {result1.bull}, Cows: {result1.cow}");
                if (result1.bull == 4)
                {
                    Console.WriteLine("Player 1 wins!");
                    break;
                }

                var guess2 = ReadValidNumber("Player 2", "make your guess");
                var result2 = Compare(secrete1, guess2);
                Console.WriteLine($"Player 2 -> Bulls: {result2.bull}, Cows: {result2.cow}");
                if (result2.bull == 4)
                {
                    Console.WriteLine("Player 2 wins!");
                    break;
                }

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();

            }
        }

        private string ReadValidNumber(string playerName, string prompt)
        {
            string input;

            while (true)
            {
                Console.Write($"{playerName}, {prompt}: ");
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input) || input.Length != 4)
                {
                    Console.WriteLine("Input must be exactly 4 digits.");
                    continue;
                }

                if (!input.All(char.IsDigit))
                {
                    Console.WriteLine("Input must contain only digits (0-9).");
                    continue;
                }

                if (input.Distinct().Count() != 4)
                {
                    Console.WriteLine("All digits must be unique. Try again.");
                    continue;
                }

                break;
            }

            return input;
        }

        private (int bull, int cow) Compare(string secrete, string guess)
        {

            int bull = 0, cow = 0;
            for (int i = 0; i < guess.Length; i++)
            {
                if (secrete[i] == guess[i])
                {
                    bull++;
                }
                else if (secrete.Contains(guess[i]))
                {
                    cow++;
                }
            }
            return (bull, cow);
        }

        protected override string GenerateSecret()
        {
            throw new NotImplementedException();
        }

        protected override string GetGuess()
        {
            throw new NotImplementedException();
        }

        protected override void GetResponse((int bull, int cow) bullAndCow)
        {
            throw new NotImplementedException();
        }
    }
}
