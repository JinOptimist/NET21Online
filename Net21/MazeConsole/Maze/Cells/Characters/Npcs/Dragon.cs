using MazeConsole.Maze.Cells.Surface;

namespace MazeConsole.Maze.Cells.Сharacters.Npcs
{
    /// <summary>
    /// Dragon NPC that moves up to 3 cells away, chases the Hero if nearby,
    /// deals damage on contact, and leaves 5 coins when defeated.
    /// </summary>
    public class Dragon : BaseNpc
    {
        /// <summary>
        /// Creates a new Dragon at the specified position with given health and money.
        /// </summary>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        /// <param name="mazeMap">Reference to the maze map.</param>
        /// <param name="hp">Health points.</param>
        /// <param name="money">Amount of money.</param>
        public Dragon(int x, int y, IMazeMap mazeMap, int hp, int money) : base(x, y, mazeMap, hp, money)
        {
            Hp = hp;
            Money = money;
        }

        public override string Symbol => "D";

        public override BaseCell? CellToMove()
        {
            var nearCells = MazeMap.GetCellsInRadius(this, 3);

            var hero = nearCells.OfType<Hero>().FirstOrDefault();
            if (hero != null)
            {
                return hero;
            }

            var grounds = nearCells.OfType<Ground>().ToList();
            if (!grounds.Any())
            {
                return null;
            }

            return grounds.First();
        }

        public override bool TryStep(IBaseCharacter character)
        {
            if (character is Dragon)
            {
                return true;
            }

            if (character is Hero || character is IHero)
            {
                character.Hp -= 3;
                Hp--;
            }

            if (Hp <= 0)
            {
                MazeMap.Npcs.Remove(this);
                var coin = new Coin(X, Y, MazeMap, 5);
                MazeMap.ReplaceCell(coin);
            }

            return false;
        }
    }
}