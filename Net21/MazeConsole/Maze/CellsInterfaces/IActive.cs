namespace MazeConsole.Maze.CellsInterfaces;


/// <summary>
/// implements the property of activity in the cell, the reaction to which can be processed
/// </summary>
public interface IActive
{
    bool IsActive { get; set; }

    string GetSymbol();
}