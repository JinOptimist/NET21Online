using MazeConsole.Maze.Cells;
using MazeConsole.Maze.Cells.Сharacters;
using MazeConsole.Maze.Cells.Сharacters.Npcs;

namespace MazeConsole.Maze
{
    public interface IMazeMap
    {
        BaseCell? this[int x, int y] { get; }

        List<BaseCell> CellsSurface { get; init; }
        int Height { get; init; }
        Hero Hero { get; set; }
        MazeMap NextLevel { get; set; }
        List<BaseNpc> Npcs { get; init; }
        MazeMap PrevLevel { get; set; }
        int Width { get; init; }

        void AddNpcRequest(BaseNpc npc);
        IEnumerable<BaseCell> GetCellsInRadius(BaseCell cell, int radius);
        IEnumerable<BaseCell> GetNearCell(BaseCell cell);
        void ProcessNpcRequests();
        void ReplaceCell(BaseCell newCell);
    }
}