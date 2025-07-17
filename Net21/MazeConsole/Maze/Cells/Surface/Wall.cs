using MazeConsole.Maze.Cells.Сharacters;

namespace MazeConsole.Maze.Cells.Surface
{
    public class Wall : BaseCell
    {
        public Wall(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public override string Symbol => "#";

        public override bool TryStep(IBaseCharacter hero)
        {
            return false;
        }
    }
}
