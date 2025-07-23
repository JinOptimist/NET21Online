namespace MazeConsole.Maze.Cells.Items
{
    /// <summary>
    /// Class for items that can be in inventory
    /// </summary>
    public abstract class BaseItems : BaseCell
    {
        protected BaseItems(int x, int y, IMazeMap mazeMap, string name) : base(x, y, mazeMap)
        {
            Name = name;
        }

        public string Name { get; set; }

    }
}
