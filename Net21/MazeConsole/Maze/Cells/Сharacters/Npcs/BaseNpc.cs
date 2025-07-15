using MazeConsole.Maze.Cells.Сharacters;

namespace MazeConsole.Maze.Cells.Сharacters.Npcs
{
    public abstract class BaseNpc : BaseCharacter
    {
        public BaseNpc(int x, int y, MazeMap mazeMap, int hp, int maney) : base(x, y, mazeMap)
        {
        }

        public abstract BaseCell? CellToMove();
    }
}
