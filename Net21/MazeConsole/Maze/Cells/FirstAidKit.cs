using MazeConsole.Maze.Cells.Сharacters;


namespace MazeConsole.Maze.Cells
{
    public class FirstAidKit:BaseCell
    {
        public FirstAidKit(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public override string Symbol => "+";

        public override bool TryStep(BaseCharacter character)
        {
            character.Hp += 2;
            var ground = new Ground(X, Y, MazeMap);
            MazeMap.ReplaceCell(ground);
            return true;
        }
    }
}
