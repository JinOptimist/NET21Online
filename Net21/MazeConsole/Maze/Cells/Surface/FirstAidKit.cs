using MazeConsole.Maze.Cells.Characters;

namespace MazeConsole.Maze.Cells.Surface
{
    public class FirstAidKit : BaseCell
    {
        public FirstAidKit(int x, int y, IMazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public override string Symbol => "+";

        public override bool TryStep(IBaseCharacter character)
        {
            character.Hp += 2;
            var ground = new Ground(X, Y, MazeMap);
            MazeMap.ReplaceCell(ground);
            return true;
        }
    }
}
