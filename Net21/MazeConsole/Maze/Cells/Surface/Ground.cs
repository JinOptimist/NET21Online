using MazeConsole.Maze.Cells.Сharacters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsole.Maze.Cells.Surface
{
    public class Ground : BaseCell
    {
        public Ground(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public override string Symbol => ".";

        public override bool TryStep(BaseCharacter hero)
        {
            return true;
        }
    }
}