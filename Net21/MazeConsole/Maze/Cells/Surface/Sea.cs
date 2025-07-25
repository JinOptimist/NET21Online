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
        public Sea(int x, int y, IMazeMap mazeMap) : base(x, y, mazeMap)
        {

        }

        public override string Symbol => "~";

        public override bool TryStep(IBaseCharacter character)
        {
            if (character is IHero)
            {
                var hero = (IHero)character;
                return hero.Inventory.Any(item => item is IBoat);
            }

            if (character is Goblin)
            {
                return true;
            }

            return false;
        }
    }
}
