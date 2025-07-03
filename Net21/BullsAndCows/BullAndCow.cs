using static BullsAndCows.SettingsGame;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BullsAndCows
{
    public abstract class BullAndCowBase
    {
        private string _secret;

        public void Play()
        {

            _secret = GenerateSecret();

            string guess;
            do
            {
                guess = GetGuess();

                if (!ValidateFourDigitNumber(guess))
                {
                    continue;
                }

                if(guess == _secret)
                {
                    break;
                } 

                var bullAndCow = CalculateBullAndCow(guess);
                GetAnswer(bullAndCow);

            } while (guess != _secret);

            EndGame(_secret);
        }

        protected abstract void EndGame(string secret);

        protected bool ValidateFourDigitNumber(string? guess)
        {
            if (string.IsNullOrWhiteSpace(guess))
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("Input is empty. Please enter a 4-digit number.");
                Console.WriteLine();
                return false;
            }

            if (guess.Length != 4)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("Input must be exactly 4 characters long.");
                Console.WriteLine();
                return false;
            }

            if (!int.TryParse(guess, out _))
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("Please enter a valid 4-digit number (0-9 only).");
                Console.WriteLine();
                return false;
            }

            if (guess.Distinct().Count() != 4)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("All digits must be unique.");
                Console.WriteLine();
                return false;
            }

            return true;
        }

        protected abstract void GetAnswer((int bull, int cow) bullAndCow);

        private (int bull, int cow) CalculateBullAndCow(string guess)
        {
            int bull = 0;
            int cow = 0;

            for (int i = 0; i < guess.Length; i++)
            {
                var guess_symvol = guess[i];
                for (int j = 0; j < _secret.Length; j++)
                {
                    var _secret_symvol = _secret[j];
                    if (guess_symvol == _secret_symvol)
                    {
                        if (i == j)
                        {
                            bull++;
                            continue;
                        }

                        cow++;
                    }
                }
            }


            return (bull, cow);
        }

        protected abstract string GetGuess();

        protected abstract string GenerateSecret();
    }

}
