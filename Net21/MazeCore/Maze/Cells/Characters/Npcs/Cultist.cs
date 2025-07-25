using MazeCore.Maze;
using MazeCore.Maze.Cells;
using MazeCore.Maze.Cells.Characters;
using MazeCore.Maze.Cells.Surface;

namespace MazeCore.Maze.Cells.Characters.Npcs
{
    public class Cultist : BaseNpc
    {
        public Cultist(int x, int y, IMazeMap mazeMap) : base(x, y, mazeMap)
        {
            Hp = 1;
            _isScared = false;
            _stepsToInvokeBase = 5;
            _stepsToInvokeCounter = _stepsToInvokeBase;
        }

        public override string Symbol => "u";

        private bool _isScared;

        private readonly int _stepsToInvokeBase;
            
        private int _stepsToInvokeCounter;
        private int StepsToInvokeCounter
        {
            get => _stepsToInvokeCounter;
            set
            {
                _stepsToInvokeCounter = value;
                
                if (_stepsToInvokeCounter <= 0)
                {
                    InvokeThing();
                    _stepsToInvokeCounter = _stepsToInvokeBase;
                }
            }
        }

        public override BaseCell? CellToMove()
        {
            var neraCells = MazeMap
                .GetNearCell(this);
            var hero = neraCells.OfType<Hero>().FirstOrDefault();
            if (hero != null)
            {
                _isScared = true;
            }

            var grounds = MazeMap
                .GetNearCell(this)
                .OfType<Ground>();

            if (_isScared)
            {
                return grounds.First();
            }

            StepsToInvokeCounter--;

            return null;
        }

        public override bool TryStep(IBaseCharacter character)
        {
            if (character is EvilSpirit)
            {
                return true;
            }
            
            return false;
        }

        private void InvokeThing()
        {
            var evilSprit = new EvilSpirit(X, Y, MazeMap);
            MazeMap.AddNpcRequest(evilSprit);
        }
    }
}