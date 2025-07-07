namespace BullsAndCows
{
    /// <summary>
    /// Human set secret. Bot try to get answer
    /// </summary>
    public class BullAndCowHumanVsBot : BullAndCowBase
    {

        private List<string> _possibleNumbers;
        private string? _currentGuess;
        private List<string> _previousGuesses = new List<string>();
        private bool _isGameOver = false;

        public BullAndCowHumanVsBot()
        {
            InitializePossibleNumbers();
        }

        private void InitializePossibleNumbers()
        {
            _possibleNumbers = new List<string>();

           for (int num = 1023; num <= 9876; num++)
            {
                var numStr = num.ToString();
                if (numStr.Distinct().Count() == 4)
                {
                    _possibleNumbers.Add(numStr);
                }
            }
        }

        protected override string GenerateSecret()
        {
            Console.WriteLine("Please think of a 4-digit number with unique digits.");
            Console.WriteLine("I'll try to guess it! Just tell me how many bulls and cows I get each time.");
            return "";
        }

        protected override string GetGuess()
        {
            if (_isGameOver)
            {
                return null;
            }
            if (_previousGuesses.Count == 0)
            {
                _currentGuess = "1234";
            }
            else
            {
                _currentGuess = GetOptimalGuess();
            }
            _previousGuesses.Add(_currentGuess);

            Console.WriteLine($"My guess is: {_currentGuess}");
            Console.WriteLine("How many bulls and cows did I get?");
            return _currentGuess;
        }

        private string GetOptimalGuess()
        {
            if (_possibleNumbers.Count <= 2)
            {
                return _possibleNumbers.First();
            }

            if (_possibleNumbers.Count < 10)
            {
                return _possibleNumbers[0];
            }
            string bestGuess = null;
            int minMaxRemaining = int.MaxValue;
            foreach (var guess in _possibleNumbers)
            {
                var responseGroups = new Dictionary<(int bulls, int cows), int>();

                foreach (var possibleSecret in _possibleNumbers)
                {
                    var response = CalculateBullsAndCows(guess, possibleSecret);
                    if (responseGroups.ContainsKey(response))
                    {
                        responseGroups[response]++;
                    }
                    else
                    {
                        responseGroups[response] = 1;
                    }
                }
                int maxRemaining = responseGroups.Values.Max();
                if (maxRemaining < minMaxRemaining)
                {
                    minMaxRemaining = maxRemaining;
                    bestGuess = guess;
                }
            }

            return bestGuess;
        }

        private (int bulls, int cows) CalculateBullsAndCows(string guess, string secret)
        {
            int bulls = 0;
            int cows = 0;

            for (int i = 0; i < 4; i++)
            {
                if (guess[i] == secret[i])
                {
                    bulls++;
                }
                else if (secret.Contains(guess[i]))
                {
                    cows++;
                }
            }

            return (bulls, cows);
        }



        protected override void GetResponse((int bull, int cow) bullAndCow)
        {
            if (_isGameOver) 
            {
                return;
            }
            (int bulls, int cows) response = GetBullsAndCowsFromUser();

            _possibleNumbers = _possibleNumbers
                .Where(num => CalculateBullsAndCows(_currentGuess, num) == response)
                .ToList();

            if (_possibleNumbers.Count == 0)
            {
                Console.WriteLine("Error: No possible options. Perhaps you made a mistake in your answers.");
                throw new InvalidOperationException("Impossible combination of answers");
            }

            if (response.bulls == 4)
            {
                Console.WriteLine($"Hooray! I guessed your {_currentGuess} number in {_previousGuesses.Count} tries!");
                _isGameOver = true;
                return;
            }

        }

        private (int bulls, int cows) GetBullsAndCowsFromUser()
        {
            while (true)
            {
                Console.Write("Enter the number of bulls (0-4): ");
                if (!int.TryParse(Console.ReadLine(), out int bulls) || bulls < 0 || bulls > 4)
                {
                    Console.WriteLine("Incorrect input! Enter a number between 0 and 4");
                    continue;
                }

                Console.Write("Enter the number of cows (0-4): ");
                if (!int.TryParse(Console.ReadLine(), out int cows) || cows < 0 || cows > 4)
                {
                    Console.WriteLine("Incorrect input! Enter a number between 0 and 4");
                    continue;
                }

                if (bulls + cows > 4)
                {
                    Console.WriteLine("The sum of bulls and cows cannot exceed 4!");
                    continue;
                }

                return (bulls, cows);
            }
        }
    }
}
