using System;

namespace MazeConsole.Maze.Cells
{
    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public class FirstAidKit : BaseCell
    {
        public FirstAidKit(int x, int y, MazeMap mazeMap) : base (x, y, mazeMap)
        {           
        }

        public override string Symbol => "+";

        public override bool TryStep(Hero hero)
        {
            hero.Hp += 2;
            var ground = new Ground(X, Y, MazeMap);
            MazeMap.ReplaceCell(ground);
            return true;
        }
    }
}

