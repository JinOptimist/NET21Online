

namespace FirstConsole
{
    public class GameGuessTheNumber
    {
        private int _min;
        private int _max;
        private int _attempt;
        private int _maxAttempt;
        private int _theNumber;
        private bool _isWin = false;

        public void Play()
        {
            var gameMode = (GameMode)GetNumberFromUser("Enter 1 if you want play with bot. " +
                "\nEtner 2 if you have a friend");

            switch (gameMode)
            {
                case GameMode.CreateRuleByBot:
                    SetRulesByBot();
                    break;
                case GameMode.CreateRuleByUser:
                    SetRulesByUser();
                    break;
                default:
                    Console.WriteLine("YOU ARE BAD");
                    return;
            }

            GamePlay();

            EndGame();
        }

        private void SetRulesByBot()
        {
            _min = 1;
            _max = GetRandomNumberBetween(100, 200);
            _theNumber = GetRandomNumberBetween(_min, _max);

            _attempt = 1;
            _maxAttempt = CalculateMaxAttempt(_min, _max);

            Console.Clear();
        }

        private int GetRandomNumberBetween(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }

        private void SetRulesByUser()
        {
            _min = GetNumberFromUser("Enter min value for the number");
            _max = GetNumberFromUser("Enter max value for the number", _min);
            _theNumber = GetNumberFromUser("Enter THE NUMBER", _min, _max);

            _attempt = 1;
            _maxAttempt = CalculateMaxAttempt(_min, _max);

            Console.Clear();
        }

        private void EndGame()
        {
            if (_isWin)
            {
                Console.WriteLine("Win");
            }
            else
            {
                Console.WriteLine("Loose");
            }
        }

        private void GamePlay()
        {
            int guess;
            do
            {
                guess = GetNumberFromUser(
                    $"Enter your guess. Interval [{_min}, {_max}] Attempt {_attempt}/{_maxAttempt}",
                    _min,
                    _max);

                _attempt++;
                if (guess > _theNumber)
                {
                    Console.WriteLine("My number is less");
                    _max = guess - 1;
                }
                else if (guess < _theNumber)
                {
                    Console.WriteLine("My number is bigger");
                    _min = guess + 1;
                }
                else // guess == _theNumber
                {
                    _isWin = true;
                }
            } while (guess != _theNumber && _attempt <= _maxAttempt);
        }

        private int CalculateMaxAttempt(int min, int max)
        {
            var hisIntevalLenght = max - min;
            var step = 1;
            var myInteval = 1;

            while (myInteval < hisIntevalLenght)
            {
                step++;
                myInteval *= 2;
            }

            return step;
        }

        private int GetNumberFromUser(string infoMessageForTheUser)
        {
            return GetNumberFromUser(infoMessageForTheUser, int.MinValue, int.MaxValue);
        }

        private int GetNumberFromUser(string infoMessageForTheUser, int min)
        {
            return GetNumberFromUser(infoMessageForTheUser, min, int.MaxValue);
        }

        private int GetNumberFromUser(string infoMessageForTheUser, int min, int max)
        {
            string text;
            int number;
            bool doINeedContinue;
            do
            {
                Console.WriteLine(infoMessageForTheUser);
                text = Console.ReadLine();
                var isItNumber = int.TryParse(text, out number);
                doINeedContinue = !isItNumber || number < min || number > max;
            } while (doINeedContinue);

            return number;
        }
    }
}
