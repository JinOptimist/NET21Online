using MazeConsole.Maze.Cells.Surface;

namespace MazeConsole.Maze.Cells.Сharacters.Npcs
{
    public class Goblin : BaseNpc
    {
        public Goblin(int x, int y, MazeMap mazeMap, int hp = 2) : base(x, y, mazeMap)
        {
            Hp = hp;
        }

        public override string Symbol => "g";

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

        public override bool TryStep(BaseCharacter character)
        {
            if (character is Goblin)
            {
                return true;
            }

            character.Hp--;
            return false;
        }
    }
}
