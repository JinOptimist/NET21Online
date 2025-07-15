using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MazeConsole.Maze.Cells
{
    public class Shield : BaseCell
    {
        /// <summary>
        /// Shield add +2 HP for user
        /// </summary>

        public Shield(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public override string Symbol => "/";

        public override bool TryStep(Hero hero)
        {
            hero.Hp += 2;
            var ground = new Ground(X, Y, MazeMap);
            MazeMap.ReplaceCell(ground);
            return true;
        }
    }
}

