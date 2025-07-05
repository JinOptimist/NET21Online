using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows
{

    public class Start()
    {
        public void Play()
        {

            BullAndCowBase game = null;

            while (game == null)
            {
                var gameMode = (GameMode)GameModeChoise();
                switch (gameMode)
                {
                    case GameMode.BullAndCowBotVsHuman:
                        game = new BullAndCowBotVsHuman();
                        break;
                    case GameMode.BullAndCowHumanVsHuman:
                        game = new BullAndCowHumanVsHuman();
                        break;
                    default:
                        Console.WriteLine("You entered an invalid value.");
                        continue;
                }
                game.Play();
            }
        }

        private int GameModeChoise()
        {
            Console.WriteLine("You can Play with real user or Bot. " +
            "\nIf you would play with Bot, please enter 1. iF you would play with human, please enter 2");
            bool isItNumber;

            string inputText = Console.ReadLine();
            isItNumber = int.TryParse(inputText, out int number);


            while (!isItNumber)
            {
                Console.WriteLine("You should enter number only number 1 or 2. Try Again.");
                inputText = Console.ReadLine();
                isItNumber = int.TryParse(inputText, out number);
            }
            return number;
        }
    }
}

