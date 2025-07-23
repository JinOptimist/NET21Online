using MazeConsole.Maze.Cells.Characters;
using MazeConsole.Maze.Cells.Surface;

namespace MazeConsole.Maze.Cells.Characters.Npcs
{
    public class Snow : BaseNpc
    {
        public Snow(int x, int y, MazeMap mazeMap, int hp = 1, int money= 1) : base(x, y, mazeMap)
        {
            Hp = hp;
            Money = money;
        }

        public override string Symbol => "o";

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