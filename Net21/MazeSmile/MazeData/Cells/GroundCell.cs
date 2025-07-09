namespace MazeSmile.MazeData.Cells
{
    public class GroundCell : BaseCell
    {
        public override bool CanStep => true;
        public override void OnStep(Player player)
        {
            // Ground: can step, maybe add logic later
        }
    }
}
