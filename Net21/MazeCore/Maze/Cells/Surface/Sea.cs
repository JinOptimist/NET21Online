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
