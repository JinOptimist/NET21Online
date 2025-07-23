using MazeConsole.Maze.Cells.Surface;

namespace MazeConsole.Maze.Cells.Сharacters.Npcs
{
    public class Goblin : BaseNpc
    {
        public Goblin(int x, int y, IMazeMap mazeMap, int hp, int money) : base(x, y, mazeMap, hp, money)
        {
            Hp = hp;
            Money = money;
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

        public override bool TryStep(IBaseCharacter character)
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
