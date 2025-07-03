using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows
{
    public class SettingsGame
    {
        public enum Gamemode
        {
            Bot = 1,
            Player = 2,
            BotVSYou = 3,
        }

        public void SwitchPlayer()
        {
            Console.WriteLine("Select game mode [Select number]:");

            foreach (Gamemode mode in Enum.GetValues(typeof(Gamemode)))
            {
                Console.WriteLine($"{(int)mode}) Game with {mode}");
            }

            var input = Console.ReadLine();

            if (int.TryParse(input, out int selectedGamemode))
            {
                var gamemode = (Gamemode)selectedGamemode;

                switch (gamemode)
                {
                    case Gamemode.Bot:
                        Play(new BullAndCowHumanVsBot(), "Starting Player vs Bot mode.");
                        break;
                    case Gamemode.Player:
                        Play(new BullAndCowHumanVsHuman(), "Starting Player vs Player mode.");
                        break;
                    case Gamemode.BotVSYou:
                        Play(new BullAndCowBotVsHuman(), "Starting Bot vs Player mode.");
                        break;

                    default: Error("This number is not in the list. Try again."); break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Error! Invalid input. Please enter a number.");
                Console.WriteLine();
                SwitchPlayer();
            }
        }

        private void Play(BullAndCowBase game, string mess = "-------------------------------------------")
        {
            Console.Clear();
            Console.WriteLine(mess);
            game.Play();
        }

        private void Error(string mess)
        {
            Console.Clear();
            Console.WriteLine(mess);
            Console.WriteLine();
            SwitchPlayer();
        }
    }
}
