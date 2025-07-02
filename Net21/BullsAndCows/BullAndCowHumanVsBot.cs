namespace BullsAndCows
{
    /// <summary>
    /// Human set secret. Bot try to get answer
    /// </summary>
    public class BullAndCowHumanVsBot : BullAndCowBase
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
