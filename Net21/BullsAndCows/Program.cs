using BullsAndCows;

internal class Program
{
    //    bool keepRunning = true;

    //    while (keepRunning)
    //    {
    //        int gamesel;
    //        bool validChoice;

    //        do
    //        {
    //            Console.WriteLine("Choose a game:");
    //            Console.WriteLine("1. Bull and Cow (Bot vs Human)");
    //            Console.WriteLine("2. Bull and Cow (Human vs Human)");
    //            Console.WriteLine("3. Bull and Cow (Human vs Bot)");
    //            Console.WriteLine("4. Exit the game");

    //            validChoice = int.TryParse(Console.ReadLine(), out gamesel);
    //            if (!validChoice || gamesel < 1 || gamesel > 4)
    //            {
    //                Console.WriteLine("Error !!! Enter the number shown on the screen");
    //            }
    //        } while (!validChoice || gamesel < 1 || gamesel > 4);

    //        if (gamesel == 4)
    //        {
    //            keepRunning = false;
    //            continue;
    //        }

    //        bool replayCG = true;
    //        while (replayCG && keepRunning)
    //        {
    //            Console.Clear();

    //            switch (gamesel)
    //            {
    //                case 1:
    //                    var game = new BullAndCowBotVsHuman();
    //                    game.Play();
    //                    break;
    //                case 2:
    //                    Console.WriteLine("Soon...");
    //                    break;
    //                case 3:
    //                    Console.WriteLine("Soon...");
    //                    break;
    //            }

    //            Console.WriteLine("YOUR NEXT STEPS:");
    //            Console.WriteLine("1. Play again");
    //            Console.WriteLine("2. Change game mode");
    //            Console.WriteLine("3. Return to main menu");
    //            Console.WriteLine("4. Exit the game");

    //            int actionChoice;
    //            while (!int.TryParse(Console.ReadLine(), out actionChoice) || actionChoice < 1 || actionChoice > 4)
    //            {
    //                Console.Write("Incorrect input. Select 1-4: ");
    //            }

    //            switch (actionChoice)
    //            {
    //                case 1:
    //                    replayCG = true;
    //                    break;
    //                case 2:
    //                    replayCG = false;
    //                    break;
    //                case 3:
    //                    replayCG = false;
    //                    keepRunning = true;
    //                    break;
    //                case 4:
    //                    replayCG = false;
    //                    keepRunning = false;
    //                    break;
    //            }
    //        }
    //    }
    //    Console.WriteLine("\nTnx for playing!");
    //    Thread.Sleep(2000);
    //}




    private static void Main(string[] args)
    {
        bool keepRuning = true;
        while (keepRuning)
        {

            int gameset;
            bool valueChoise;
            do
            {
                Console.WriteLine("Choose a game:");
                Console.WriteLine("1. Bull and Cow (Bot vs Human)");
                Console.WriteLine("2. Bull and Cow (Human vs Human)");
                Console.WriteLine("3. Bull and Cow (Human vs Bot)");
                Console.WriteLine("4. Exit the game");

                valueChoise = int.TryParse(Console.ReadLine(), out gameset);

                if (!valueChoise || gameset < 1 || gameset > 4)
                {
                    Console.WriteLine("Error !!! Enter the number shown on the screen");
                }

            }

            while (!valueChoise || gameset < 1 || gameset > 4);

            if (gameset == 4)
            {
                keepRuning = false;
                continue;
            }

            bool replayCG = true;
            while (replayCG && keepRuning)
            {

                Console.Clear();

                switch (gameset)
                {
                    case 1:
                        var game = new BullAndCowBotVsHuman();
                        game.Play();
                        break;
                    case 2:
                        var game2 = new BullAndCowHumanVsHuman();
                        game2.Play();
                        break;
                    case 3:
                        var game3 = new BullAndCowHumanVsBot();
                        game3.Play();
                        break;

                }

                Console.WriteLine("YOUR NEXT STEPS:");
                Console.WriteLine("1. Play again");
                Console.WriteLine("2. Change game mode");
                Console.WriteLine("3. Return to main menu");
                Console.WriteLine("4. Exit the game");

                int actionChooise;

                while (!int.TryParse(Console.ReadLine(), out actionChooise) || actionChooise < 1 || actionChooise > 4)
                {
                    Console.Write("Incorrect input. Select 1-4: ");
                }

                switch (actionChooise)
                {
                    case 1:
                        replayCG = true;
                        break;
                    case 2:
                        replayCG = false;
                        break;
                    case 3:
                        replayCG = false;
                        keepRuning = true;
                        break;
                    case 4:
                        replayCG = false;
                        keepRuning = false;
                        break;
                }
            }
        }
        Console.WriteLine("\nTnx for playing!");
        //Thread.Sleep(2000);
    }
}