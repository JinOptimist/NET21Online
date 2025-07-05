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
                var bullAndCow = CalculateBullAndCow(guess);
                GetResponse(bullAndCow);
            } while (guess != _secret);

            Console.WriteLine($"Win. Number was {_secret}");
        }

        protected abstract void GetResponse((int bull, int cow) bullAndCow);

        private (int bull, int cow) CalculateBullAndCow(string guess)
        {
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
