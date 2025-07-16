using MazeConsole.Maze.Cells.Сharacters;

namespace MazeConsole.Maze.Cells
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
