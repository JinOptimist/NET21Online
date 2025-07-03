namespace BullsAndCows
{
    /// <summary>
    /// Bot set secret. Human try to get answer
    /// </summary>
    public class BullAndCowHumanVsBot : BullAndCowBase
    {
        
        protected override string GenerateSecret()
        {
            var secret = "";
            string allNumbers = "0123456789";
            var random = new Random();

            for (int i = 0; i < 4; i++)
            {
                var symvol = random.Next(allNumbers.Length);
                secret = secret + allNumbers[symvol];
                allNumbers = allNumbers.Replace(allNumbers[symvol].ToString(), "");
            }

            return secret;
        }

        protected override void GetAnswer((int bull, int cow) bullAndCow)
        {
            Console.WriteLine($"Bulls: {bullAndCow.bull}");
            Console.WriteLine($"Cows: {bullAndCow.cow}");
            Console.WriteLine();
        }

        protected override string GetGuess()
        {
            Console.WriteLine("Write number with 4 diff digits.");

            return Console.ReadLine();
        }

        protected override void EndGame(string secret)
        {
            Console.Clear();
            Console.WriteLine($"You win. Number was {secret}");
        }
    }
}
