using MazeConsole.Maze.Cells.Сharacters;

namespace MazeConsole.Maze.Cells.Inventory
{
    public interface IBaseItems
    {
        string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public IMazeMap MazeMap { get; set; }
        
        bool TryStep(IBaseCharacter character);
    }
}