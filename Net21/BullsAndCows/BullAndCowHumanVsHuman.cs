using System.Text.RegularExpressions;


namespace BullsAndCows
{
    /// <summary>
    /// Human set secret. Human try to get answer
    /// </summary>
    internal class BullAndCowHumanVsHuman : BullAndCowBase
    {
        protected override string GenerateSecret()
        {
            var answer = string.Empty;
            var isCorrect = false;

            Console.WriteLine("Enter secret number: ");
            do
            {
                answer = Console.ReadLine();
                
                //check on length
                if (answer.Length != 4)
                {
                    Console.WriteLine("|   the number length must be 4!!!");
                    continue;
                }
                
                //check on letters
                if (!Regex.IsMatch(answer, @"\d{4}"))
                {
                    Console.WriteLine("|   the number contains letters!!!");
                    continue;
                }

                isCorrect = true;
                
                //check on same digits
                for (int d = 0; d < answer.Length; d++)
                {
                    for (int i = 0; i < answer.Length; i++)
                    {
                        if (i != d)
                        {
                            if (answer[i] == answer[d])
                            {
                                Console.WriteLine("|   the number has the same digits!!!");
                                isCorrect = false;
                                break;
                            }
                        }
                    }

                    if (!isCorrect)
                    {
                        break;
                    }
                }
            } while (!isCorrect);
            
            Console.Clear();

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
