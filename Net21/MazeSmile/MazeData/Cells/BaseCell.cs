namespace MazeSmile.MazeData.Cells
{
    public abstract class BaseCell
    {
        public abstract bool CanStep { get; }
        public abstract void OnStep(Player player);
    }
}
