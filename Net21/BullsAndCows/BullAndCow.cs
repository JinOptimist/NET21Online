using static System.Net.Mime.MediaTypeNames;

namespace BullsAndCows
{
    public abstract class BullAndCowBase
    {
        private string _secret;

        public void Play()
        {
            _secret = GenerateSecret();

            string guess;
            var attempt = 0;
            do
            {
                guess = GetGuess();
                var bullAndCow = CalculateBullAndCow(guess);
                GetResponse(bullAndCow);
                attempt++;
            } while (guess != _secret);

            Console.WriteLine($"Win. Number was {_secret}. You made {attempt} attempts");
        }


        protected abstract void GetResponse((int bull, int cow) bullAndCow);

        private (int bull, int cow) CalculateBullAndCow(string guess)
        {
            // _secret  == "1294"
            // guess    == "1348"

            // text = "smile"
            // text[2] == "i"
            // text[0] == "s"
            var bull = 0;
            var cow = 0;
            for (int i = 0; i < guess.Length; i++)
            {
                var guessSymbol = guess[i];

                var position = -1;
                for (int k = 0; k < _secret.Length; k++)
                {
                    var secretSymbol = _secret[k];
                    if (secretSymbol == guessSymbol)
                    {
                        position = k;
                        break;
                    }
                }

                if (position == i)
                {
                    bull++;
                }
                else if (position >= 0)
                {
                    cow++;
                }
            }

            return (bull, cow);
        }

        protected abstract string GetGuess();

        protected abstract string GenerateSecret();


    }
}


