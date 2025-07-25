using MazeCore.Maze.Cells.Characters;

namespace MazeCore.Maze.Cells.Surface
{
    public class HealingWell : BaseCell  
    {
        public HealingWell(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
        }
       
        public override string Symbol => "W";

        public override bool TryStep(IBaseCharacter hero)
        {
            hero.Hp++;
            return true;
        }
    }
}
