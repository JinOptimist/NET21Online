namespace MazeConsole.Maze.Cells.Сharacters
{
    public abstract class BaseCharacter : BaseCell
    {
        public BaseCharacter(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public int Money { get; set; }
        public int Hp { get; set; }
    }
}
