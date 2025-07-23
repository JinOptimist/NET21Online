using MazeCore.Maze.Cells.Characters;
using MazeCore.Maze.Cells.Surface;

namespace MazeCore.Maze.Cells.Items
{
    public class Boat : BaseItems
    {
        public Boat(int x, int y, IMazeMap mazeMap, string name) : base(x, y, mazeMap, name)
        {
            Name = name;
        }

        public override string Symbol => "^";

        public override bool TryStep(IBaseCharacter character)
        {
            if (character is not Hero)
            {
                return false;
            }

            var hero = (character as Hero)!;

            if (!hero.CanGet())
            {
                return false;
            }

            var ground = new Ground(X, Y, MazeMap);
            var boat = new Boat(X, Y, MazeMap, Name);

            //Now we can pick up the boat, but we can't pick up the ground.
            hero.Inventory.Add(boat);

            MazeMap.ReplaceCell(ground);
            return true;
        }
    }
}
