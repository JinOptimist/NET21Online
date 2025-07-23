using MazeConsole.Maze.Cells.Characters;
using MazeConsole.Maze.Cells.Characters.Npcs;
using MazeConsole.Maze.Cells.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsole.Maze.Cells.Surface
{
    public class Sea : BaseCell
    {
        public Sea(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {

        }

        public override string Symbol => "~";

        public override bool TryStep(IBaseCharacter character)
        {
            if (character is Hero)
            {
                var hero = (Hero)character;
                return hero.Inventory.Any(item => item is Boat);
            }

            if (character is Goblin)
            {
                return true;
            }

            return false;
        }
    }
}
