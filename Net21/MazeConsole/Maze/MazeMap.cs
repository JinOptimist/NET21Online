using MazeConsole.Maze.Cells;
using MazeConsole.Maze.Cells.Сharacters;
using MazeConsole.Maze.Cells.Сharacters.Npcs;
using Microsoft.VisualBasic;

namespace MazeConsole.Maze
{
    /// <summary>
    /// Buisness Model which store data about cells, hero and enemies
    /// </summary>
    public class MazeMap
    {
        public int Width { get; init; }
        public int Height { get; init; }

        public Hero Hero { get; set; }
        public List<BaseCell> CellsSurface { get; init; } = new List<BaseCell>();
        public List<BaseNpc> Npcs { get; init; } = new List<BaseNpc>();

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
    }
}
