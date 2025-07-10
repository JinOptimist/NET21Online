using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsole.Maze.Cells.Inventory
{
    public class Sea : BaseCell
    {
        public Sea(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {

        }

        public override string Symbol => "~";

        public override bool TryStep(Hero hero)
        {
            return hero.Inventory.Any(item => item is Boat);
        }
    }
}
