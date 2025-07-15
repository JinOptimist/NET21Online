using MazeConsole.Maze.Cells.Surface;
using static MazeConsole.Maze.Cells.Сharacters.Npcs.Relation;

namespace MazeConsole.Maze.Cells.Сharacters.Npcs
{
    public class Goblin : BaseNpc
    {
        public Goblin(int x, int y, MazeMap mazeMap, int hp, int damage, int money) : base(x, y, mazeMap, hp, damage, money)
        {
            Hp = hp;
            Money = money;
            Damage = damage;
            Relation = RelationType.Enemy;
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

            character.Hp -= Damage;
            return false;
        }
    }
}
