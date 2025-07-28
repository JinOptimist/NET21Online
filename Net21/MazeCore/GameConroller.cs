using MazeCore.Maze;
using MazeCore.Maze.Cells.Characters.Npcs;
using MazeCore.Maze.Cells.Surface;

namespace MazeCore
{
    public class GameConroller
    {
        /// <summary>
        /// Return True if Hero is alive
        /// </summary>
        /// <exception cref="MazeBuildException">MazeBuildException</exception>
        /// <param name="maze"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool OneTurn(MazeMap maze, Direction direction)
        {
            var hero = maze.Hero;
            var destinationX = hero.X;
            var destinationY = hero.Y;

            switch (direction)
            {
                case Direction.North:
                    destinationY--;
                    break;
                case Direction.West:
                    destinationX--;
                    break;
                case Direction.South:
                    destinationY++;
                    break;
                case Direction.East:
                    destinationX++;
                    break;
                default:
                    // do nothing
                    break;
            }

            var cell = maze[destinationX, destinationY];

            if (cell?.TryStep(hero) ?? false)
            {
                hero.X = destinationX;
                hero.Y = destinationY;
            }

            maze.Npcs.ToList().ForEach(npc => CheckIsAlive(maze, npc));

            maze.Npcs.ForEach(TryMove);

            maze.ProcessNpcRequests();

            return hero.Hp > 0;
        }

        private void TryMove(BaseNpc npc)
        {
            var cell = npc.CellToMove();

            if(cell != null)
            {
                if (cell.TryStep(npc))
                {
                    npc.X = cell.X;
                    npc.Y = cell.Y;
                }
            }            
        }

        private void CheckIsAlive(MazeMap maze, BaseNpc npc)
        {
            if (npc.Hp <= 0)
            {
                maze.Npcs.Remove(npc);
                var coin = new Coin(npc.X, npc.Y, maze, npc.Money);
                maze.ReplaceCell(coin);
            }
        }
    }
}
