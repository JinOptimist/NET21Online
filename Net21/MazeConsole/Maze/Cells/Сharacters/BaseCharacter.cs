namespace MazeConsole.Maze.Cells.Сharacters
{
    public abstract class BaseCharacter : BaseCell
    {
        public BaseCharacter(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public int Money { get; set; } = 1;

        private int hp = 0;

        public int Hp
        {
            get => hp;
            set => hp = Math.Max(0, value);
        }
    }
}
