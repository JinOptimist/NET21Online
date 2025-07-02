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

        protected override void GetResponse((int bull, int cow) bullAndCow)
        {
            throw new NotImplementedException();
        }
    }
}
