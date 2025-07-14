using MazeConsole.Maze.Cells.Сharacters;

namespace MazeConsole.Maze.Cells
{
    public class Thief : BaseCell  
    {
        public int StolenMoney { get; set; }
        public Thief(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
           
        }
       
        public override string Symbol => "T";

        public override bool TryStep(BaseCharacter hero)
        {
            StolenMoney += hero.Money;
            hero.Money = 0;
            return true;
        }
    }
}
