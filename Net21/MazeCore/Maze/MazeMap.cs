using MazeCore.Maze.Cells;
using MazeCore.Maze.Cells.Characters;
using MazeCore.Maze.Cells.Characters.Npcs;

namespace MazeCore.Maze
{
    /// <summary>
    /// Buisness Model which store data about cells, hero and enemies
    /// </summary>
    public class MazeMap : IMazeMap
    {
        public int Width { get; init; }
        public int Height { get; init; }

        public Hero Hero { get; set; }
        public List<BaseCell> CellsSurface { get; init; } = new List<BaseCell>();
        public List<BaseNpc> Npcs { get; init; } = new List<BaseNpc>();

        private List<BaseNpc> _requestToAddNpc = new List<BaseNpc>();

        public MazeMap PrevLevel { get; set; }
        public MazeMap NextLevel { get; set; }

        public BaseCell? this[int x, int y]
        {
            get
            {
                if (Hero?.X == x && Hero?.Y == y)
                {
                    return Hero;
                }

                var npc = Npcs
                    .FirstOrDefault(cell => cell.X == x && cell.Y == y);
                if (npc != null)
                {
                    return npc;
                }

                return CellsSurface
                    .FirstOrDefault(cell => cell.X == x && cell.Y == y);
            }
        }

        public MazeMap(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void ReplaceCell(BaseCell newCell)
        {
            var oldCell = this[newCell.X, newCell.Y];
            CellsSurface.Remove(oldCell);
            CellsSurface.Add(newCell);
        }

        public IEnumerable<BaseCell> GetNearCell(BaseCell cell)
        {
            var bottomCell = this[cell.X, cell.Y + 1];
            if (bottomCell != null)
            {
                yield return bottomCell;
            }

            var topCell = this[cell.X, cell.Y - 1];
            if (topCell != null)
            {
                yield return topCell;
            }

            var rightCell = this[cell.X + 1, cell.Y];
            if (rightCell != null)
            {
                yield return rightCell;
            }

            var leftCell = this[cell.X - 1, cell.Y];
            if (leftCell != null)
            {
                yield return leftCell;
            }
        }

        /// <summary>
        /// Returns all cells within a given radius from the specified cell.
        /// Only cells inside the map boundaries are included.
        /// </summary>
        /// <param name="cell">The center cell from which to calculate the radius.</param>
        /// <param name="radius">Radius.</param>
        /// <returns>An enumerable of cells within the specified radius.</returns>
        public IEnumerable<BaseCell> GetCellsInRadius(BaseCell cell, int radius)
        {
            for (int offsetX = -radius; offsetX <= radius; offsetX++)
            {
                for (int offsetY = -radius; offsetY <= radius; offsetY++)
                {
                    int x = cell.X + offsetX;
                    int y = cell.Y + offsetY;

                    if (Math.Abs(offsetX) + Math.Abs(offsetY) > radius)
                    {
                        continue;
                    }

                    if (x < 0 || x >= Width || y < 0 || y >= Height)
                    {
                        continue;
                    }

                    var targetCell = this[x, y];
                    if (targetCell != null)
                    {
                        yield return targetCell;
                    }
                }
            }
        }

        public void AddNpcRequest(BaseNpc npc)
        {
            _requestToAddNpc.Add(npc);
        }

        public void ProcessNpcRequests()
        {
            for (int i = 0; i < _requestToAddNpc.Count; i++)
            {
                Npcs.Add(_requestToAddNpc.ElementAt(i));
                _requestToAddNpc.RemoveAt(i);
            }
        }
    }
}
