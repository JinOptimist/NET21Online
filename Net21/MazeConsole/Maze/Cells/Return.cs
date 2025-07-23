using MazeConsole.Maze.Cells.Сharacters;

namespace MazeConsole.Maze.Cells
{
    public class Return : BaseCell
    {
        public Return(int x, int y, IMazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public override string Symbol => "<";

        public override bool TryStep(IBaseCharacter hero)
        {
            hero.X = 1;
            hero.Y = 1;
            return false;
        }
    }
}
