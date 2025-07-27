using MazeCore.Maze.Cells.Characters;

namespace MazeCore.Maze.Cells.Surface
{
    public class Lava : BaseCell
    {
        public Lava(int x, int y, IMazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public override string Symbol => "№";

        public override bool TryStep(IBaseCharacter character)
        {
            
            character.Hp = 0;
            character.Money = 0;
            return false;
        }      
    }
}