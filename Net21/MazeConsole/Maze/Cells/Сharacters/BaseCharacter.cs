using static MazeConsole.Maze.Cells.Сharacters.Npcs.Relation;

namespace MazeConsole.Maze.Cells.Сharacters
{
    public abstract class BaseCharacter : BaseCell
    {
        public BaseCharacter(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public int Money { get; set; } = 1;

        public int Damage { get; set; }

        public RelationType Relation { get; set; }

        private int hp = 0;

        public int Hp
        {
            get => hp;
            set => hp = Math.Max(0, value);
        }
    }
}
