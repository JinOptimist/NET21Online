namespace MazeSmile.MazeData.Cells
{
    public class WallCell : BaseCell
    {
        public override bool CanStep => false;
        public override void OnStep(Player player)
        {
            // Wall: nothing happens, can't step
        }
    }
}
