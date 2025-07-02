namespace BullsAndCows
{
    public class BullAndCowBotVsHuman : BullAndCowBase
    {
        protected override string GenerateSecret()
        {
            var answer = "";

            var random = new Random();

            var allNumbers = "0123456789";

            for (int i = 0; i < 4; i++)
            {
                var index = random.Next(allNumbers.Length);
                var symbol = allNumbers[index].ToString();
                answer += symbol;
                allNumbers = allNumbers.Replace(symbol, "");
            }

            return answer;
        }

        protected override string GetGuess()
        {
            Console.WriteLine("Guess my number. Enter 4 diff number");
            var asnwer = Console.ReadLine();
            return asnwer;
        }

        protected override void GetResponse((int bull, int cow) bullAndCow)
        {
            Console.WriteLine($"bull: {bullAndCow.bull} cow: {bullAndCow.cow}");
        }
    }
}
