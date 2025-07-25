using MazeCore.Maze;
using MazeCore.Maze.Cells;
using MazeCore.Maze.Cells.Characters;
using MazeCore.Maze.Cells.Surface;

namespace MazeCore.Maze.Cells.Characters.Npcs
{
    public class EvilSpirit : BaseNpc
    {
        public EvilSpirit(int x, int y, IMazeMap mazeMap) : base(x, y, mazeMap)
        {
            Hp = 1;
        }

        public override string Symbol => "e";

        public override BaseCell? CellToMove()
        {
            var neraCells = MazeMap
                .GetNearCell(this);
            var hero = neraCells.OfType<Hero>().FirstOrDefault();
            if (hero != null)
            {
                return hero;
            }

            var grounds = MazeMap
                .GetNearCell(this)
                .OfType<Ground>();
            if (!grounds.Any())
            {
                return null;
            }

            return grounds.First();
        }

        public override bool TryStep(IBaseCharacter character)
        {
            if (character is EvilSpirit)
            {
                return true;
            }

            character.Hp--;
            return false;
        }
    }
}