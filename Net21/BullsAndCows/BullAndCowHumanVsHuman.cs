namespace BullsAndCows
{
    /// <summary>
    /// Human set secret. Human try to get answer
    /// </summary>
    internal class BullAndCowHumanVsHuman : BullAndCowBase
    {
        protected override string GenerateSecret()
        {
            throw new NotImplementedException();
        }

        protected override string GetGuess()
        {
            throw new NotImplementedException();
        }

        protected override void GetAnswer((int bull, int cow) bullAndCow)
        {
            throw new NotImplementedException();
        }

        protected override void EndGame(string secret)
        {
            Console.WriteLine($"You win. Number was {secret}");
        }
    }
}
