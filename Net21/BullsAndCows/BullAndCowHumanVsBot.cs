namespace BullsAndCows
{
    /// <summary>
    /// Human set secret. Bot try to get answer
    /// </summary>
    public class BullAndCowHumanVsBot : BullAndCowBase
    {
        private readonly List<string> _allNumbers = new List<string>
        {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
        };
        private List<string> _allAvailableAnswers = new List<string>();
        private string _lastAnswer;
        public BullAndCowHumanVsBot()
        {
            FillAllAvailableAnswers();
        }

        protected override string GenerateSecret()
        {
            // Do nothing
            return "";
        }

        protected override string GetGuess()
        {
            _lastAnswer = _allAvailableAnswers.First();
            return _lastAnswer;
        }

        protected override void GetResponse((int bull, int cow) bullAndCow)
        {
            _allAvailableAnswers = _allAvailableAnswers
                .Where(answer =>
                {
                    var bullAndCowForMyAsnwer = CalculateBullAndCow(_lastAnswer, answer);
                    return bullAndCowForMyAsnwer == bullAndCow;
                })
                .ToList();
        }

        private int age = 5;
        protected override (int bull, int cow) CalculateBullAndCow(string guess)
        {
            Console.WriteLine($"My guess: {guess}");
            Console.WriteLine("How many bull and cow in that guess?");
            Console.WriteLine("[Bull] [Cow]");
            var answer = Console.ReadLine().Split(" ");
            var bull = int.Parse(answer[0]);
            var cow = int.Parse(answer[1]);
            return (bull, cow);
        }

        private void FillAllAvailableAnswers()
        {
            foreach (var number in _allNumbers)
            {
                Recurtion(number);
            }
        }

        private void Recurtion(string currentTemplate)//$"45.."
        {
            if (currentTemplate.Length == 4)
            {
                _allAvailableAnswers.Add(currentTemplate);
                return;
            }

            var currentTemplateNumbers = currentTemplate
                .ToList()
                .Select(x => x.ToString()); // ["4", "5"]
            // "0", "1", "2", "3", "6", "7", "8", "9"
            var listAvailableNumber = _allNumbers.Except(currentTemplateNumbers);

            foreach (var number in listAvailableNumber)
            {
                var nextStep = currentTemplate + number;
                Recurtion(nextStep);// "450"
            }
        }
    }
}
