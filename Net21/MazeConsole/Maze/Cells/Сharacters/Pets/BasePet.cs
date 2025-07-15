using MazeConsole.Maze.Cells.Сharacters;

namespace MazeConsole.Maze.Cells.Сharacters.Npcs
{
    public abstract class BasePet : BaseCharacter
    {

        public BasePet(int x, int y, MazeMap mazeMap, int hp, int damage, int maney) : base(x, y, mazeMap)
        {
        }

        public abstract BaseCell? CellToMove(Hero hero);
    }
}
