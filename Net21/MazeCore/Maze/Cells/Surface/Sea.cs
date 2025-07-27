using MazeConsole.Maze.Cells.Inventory;
using MazeCore.Maze;
using MazeCore.Maze.Cells;
using MazeCore.Maze.Cells.Characters;
using MazeCore.Maze.Cells.Characters.Npcs;
using MazeCore.Maze.Cells.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeCore.Maze.Cells.Surface
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
