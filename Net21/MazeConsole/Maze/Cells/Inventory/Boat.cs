using MazeConsole.Maze.Cells.Surface;
using MazeConsole.Maze.Cells.Сharacters;

namespace MazeConsole.Maze.Cells.Inventory
{
    public class Boat : BaseItems
    {
        public Boat(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {

        }

        public override string Symbol => "^";

        public override bool TryStep(BaseCharacter character)
        {
            if (character is not Hero)
            {
                return false;
            }

            var hero = character as Hero;

            if (!CanGet(hero))
            {
                return false;
            }

            var ground = new Ground(X, Y, MazeMap);
            var boat = new Boat(X, Y, MazeMap);

            //Now we can pick up the boat, but we can't pick up the ground.
            hero.Inventory.Add(boat);

            MazeMap.ReplaceCell(ground);
            return true;
        }
    }
}
