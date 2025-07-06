namespace BullsAndCows
{
    /// <summary>
    /// Human set secret. Bot try to get answer
    /// </summary>
    public class BullAndCowHumanVsBot : BullAndCowBase
    {

        private List<string> possibleGuesses;
        private string currentGuess;

        public void Play()
        {
            Console.WriteLine("=== Human vs Bot Mode ===");

            string secret = ReadValidNumber(" Enter your secret number (4 unique digits)");

            possibleGuesses = GenerateAllPossibleNumbers();

            int attempts = 0;
            while (true)
            {
                currentGuess = GetGuess();
                attempts++;

                Console.WriteLine($" Bot guess #{attempts}: {currentGuess}");
                var response = ReadResponseFromHuman(currentGuess);

                if (response.bull == 4)
                {
                    Console.WriteLine($" Bot guessed your number {currentGuess} in {attempts} tries!");
                    break;
                }

                // Удалить все неподходящие варианты
                possibleGuesses = possibleGuesses
                    .Where(x => Compare(x, currentGuess) == response)
                    .ToList();
            }
        }

        protected override string GenerateSecret()
        {
            throw new NotImplementedException();
        }

        protected override string GetGuess()
        {
            // Можно взять случайный вариант или первый
            Random rand = new Random();
            currentGuess = possibleGuesses[rand.Next(possibleGuesses.Count)];
            return currentGuess;
        }

        protected override void GetResponse((int bull, int cow) bullAndCow)
        {
            throw new NotImplementedException();
        }

        private (int bull, int cow) ReadResponseFromHuman(string guess)
        {
            int bulls, cows;

            while (true)
            {
                Console.Write($" How many bulls for {guess}? ");
                if (!int.TryParse(Console.ReadLine(), out bulls) || bulls < 0 || bulls > 4)
                {
                    Console.WriteLine(" Invalid input. Bulls must be between 0 and 4.");
                    continue;
                }

                Console.Write($"How many cows for {guess}? ");
                if (!int.TryParse(Console.ReadLine(), out cows) || cows < 0 || cows > 4 || bulls + cows > 4)
                {
                    Console.WriteLine(" Invalid input. Cows must be between 0 and 4, and bulls + cows <= 4.");
                    continue;
                }

                break;
            }

            return (bulls, cows);
        }

        private List<string> GenerateAllPossibleNumbers()
        {
            var numbers = new List<string>();
            for (int d1 = 0; d1 <= 9; d1++)
            {
                for (int d2 = 0; d2 <= 9; d2++)
                {
                    for (int d3 = 0; d3 <= 9; d3++)
                    {
                        for (int d4 = 0; d4 <= 9; d4++)
                        {
                            var num = $"{d1}{d2}{d3}{d4}";
                            if (num.Distinct().Count() == 4)
                                numbers.Add(num);
                        }
                    }
                }
            }

            return numbers;
        }

        private string ReadValidNumber(string prompt)
        {
            string input;
            while (true)
            {
                Console.Write($"{prompt}: ");
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input) || input.Length != 4)
                {
                    Console.WriteLine(" Input must be exactly 4 digits.");
                    continue;
                }

                if (!input.All(char.IsDigit))
                {
                    Console.WriteLine(" Input must contain only digits.");
                    continue;
                }

                if (input.Distinct().Count() != 4)
                {
                    Console.WriteLine(" All digits must be unique.");
                    continue;
                }

                break;
            }
            return input;
        }

        private (int bull, int cow) Compare(string secret, string guess)
        {
            int bull = 0, cow = 0;
            for (int i = 0; i < guess.Length; i++)
            {
                if (secret[i] == guess[i]) bull++;
                else if (secret.Contains(guess[i])) cow++;
            }
            return (bull, cow);
        }



    }
}
