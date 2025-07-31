using MazeCore.Builder;
using MazeCore.Maze;
using MazeCore.Maze.Cells;
using MazeCore.Maze.Cells.Characters;
using MazeCore.Maze.Cells.Surface;

namespace MazeCore.Maze.Cells.Characters.Npcs
{
    public class Goblin : BaseNpc
    {
        public Goblin(int x, int y, MazeMap mazeMap, int hp, int money) : base(x, y, mazeMap, hp, money)
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

            return MazeMap
                .GetNearCell(this)
                .GetRandomCell<Ground>();
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
