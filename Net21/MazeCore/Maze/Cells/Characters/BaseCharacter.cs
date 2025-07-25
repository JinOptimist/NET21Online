using MazeCore.Maze;

namespace MazeCore.Maze.Cells.Characters
{
    public abstract class BaseCharacter : BaseCell, IBaseCharacter
    {
        public BaseCharacter(int x, int y, IMazeMap mazeMap) : base(x, y, mazeMap)
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
