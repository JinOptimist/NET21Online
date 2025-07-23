using MazeCore.Maze;
using MazeCore.Maze.Cells;
using MazeCore.Maze.Cells.Characters;

namespace MazeCore.Maze.Cells.Characters.Npcs
{
    public abstract class BaseNpc : BaseCharacter
    {
        public BaseNpc(int x, int y, IMazeMap mazeMap, int hp, int maney) : base(x, y, mazeMap)
        {
        }

        protected BaseNpc(int x, int y, IMazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public abstract BaseCell? CellToMove();
    }
}
