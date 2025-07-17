using MazeConsole.Maze.Cells.Surface;

namespace MazeConsole.Maze.Cells.Сharacters.Npcs
{
    public class Snow : BaseNpc
    {
        public Snow(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public Snow(int x, int y, MazeMap mazeMap, int hp, int maney) : base(x, y, mazeMap, hp, maney)
        {
        }

        public override string Symbol => "o";

        public override BaseCell? CellToMove()
        {
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