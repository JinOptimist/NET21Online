using MazeCore.Maze.Cells.Characters;

namespace MazeCore.Maze.Cells.Surface
{
    public class Return : BaseCell
    {
        public Return(int x, int y, IMazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public override string Symbol => "<";

        public override bool TryStep(IBaseCharacter character)
        {
            character.X = 1;
            character.Y = 1;
            return false;
        }      
    }
}
