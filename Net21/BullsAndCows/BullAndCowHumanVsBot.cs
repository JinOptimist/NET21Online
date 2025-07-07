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

            // Используем минимаксный подход для нахождения лучшей догадки
            string bestGuess = null;
            int minMaxRemaining = int.MaxValue;

            // Проверяем каждую возможную догадку, чтобы увидеть, как она разделит оставшиеся числа
            foreach (var guess in _possibleNumbers)
            {
                // Создаем словарь для подсчета, как числа будут разделены по возможным ответам
                var responseGroups = new Dictionary<(int bulls, int cows), int>();

                foreach (var possibleSecret in _possibleNumbers)
                {
                    // Вычисляем, каким был бы ответ, если это было бы секретное число
                    var response = CalculateBullsAndCows(guess, possibleSecret);

                    // Считаем, сколько чисел дадут каждый возможный ответ
                    if (responseGroups.ContainsKey(response))
                    {
                        responseGroups[response]++;
                    }
                    else
                    {
                        responseGroups[response] = 1;
                    }
                }

                // Худший случай - это самая большая группа, которая останется после этой догадки
                int maxRemaining = responseGroups.Values.Max();

                // Мы хотим догадку с наименьшим худшим случаем
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

            // Фильтруем возможные числа
            _possibleNumbers = _possibleNumbers
                .Where(num => CalculateBullsAndCows(_currentGuess, num) == response)
                .ToList();

            if (_possibleNumbers.Count == 0)
            {
                Console.WriteLine("Ошибка: Нет возможных вариантов. Возможно, вы ошиблись в ответах.");
                throw new InvalidOperationException("Невозможная комбинация ответов");
            }

            if (response.bulls == 4)
            {
                Console.WriteLine($"Ура! Я угадал ваше число {_currentGuess} за {_previousGuesses.Count} попыток!");
                _isGameOver = true;
                return;
            }

        }

        private (int bulls, int cows) GetBullsAndCowsFromUser()
        {
            while (true)
            {
                Console.Write("Введите количество быков (0-4): ");
                if (!int.TryParse(Console.ReadLine(), out int bulls) || bulls < 0 || bulls > 4)
                {
                    Console.WriteLine("Некорректный ввод! Введите число от 0 до 4");
                    continue;
                }

                Console.Write("Введите количество коров (0-4): ");
                if (!int.TryParse(Console.ReadLine(), out int cows) || cows < 0 || cows > 4)
                {
                    Console.WriteLine("Некорректный ввод! Введите число от 0 до 4");
                    continue;
                }

                if (bulls + cows > 4)
                {
                    Console.WriteLine("Сумма быков и коров не может превышать 4!");
                    continue;
                }

                return (bulls, cows);
            }
        }
    }
}
