using MazeConsole.Maze.Cells.Inventory;
using MazeConsole.Maze.Cells.Сharacters;
using MazeConsole.Maze.Cells.Сharacters.Npcs;
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

        public override bool TryStep(BaseCharacter character)
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

            if (character is SeaMonster || character is BaseNpc)
            {
                return true;
            }

            return false;
        }
    }
}
