using MazeCore.Maze;
using MazeCore.Maze.Cells;
using MazeCore.Maze.Cells.Characters;
using MazeCore.Maze.Cells.Surface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeCore.Maze.Cells.Characters.Npcs
{
    public class Sentry : BaseNpc
    {
        private int _prevoiusStepX;
        private int _prevoiusStepY;
        private Ground _result;

        public Sentry(int x, int y, MazeMap mazeMap, int hp, int money) : base(x, y, mazeMap, hp, money)
        {
            Hp = hp;
            Money = money;
            _prevoiusStepX = x;
            _prevoiusStepY = y;
        }

        public override string Symbol => "V";

        public override BaseCell? CellToMove()
        {

            var grounds = MazeMap
               .GetNearCell(this)
               .OfType<Ground>();
            if (!grounds.Any())
            {
                return null;
            }

            var bottomGround = grounds.FirstOrDefault(cell => cell.X == X && cell.Y == Y + 1);
            var topGround = grounds.FirstOrDefault(cell => cell.X == X && cell.Y == Y - 1);
            var leftGround = grounds.FirstOrDefault(cell => cell.X == X - 1 && cell.Y == Y);
            var rightGround = grounds.FirstOrDefault(cell => cell.X == X + 1 && cell.Y == Y);
            var nextCell = grounds.FirstOrDefault(cell => cell.X == _prevoiusStepX && cell.Y == _prevoiusStepY);

            if (bottomGround != null && bottomGround != nextCell)
            {
                _result = bottomGround;
            }

            else if (topGround != null && topGround != nextCell)
            {
               _result = topGround;
            }

            else if (leftGround != null && leftGround != nextCell)
            {
                _result = leftGround;   
            }

            else if (rightGround != null && rightGround != nextCell)
            {
               _result = rightGround;
            }

            if (_result != null)
            {
                _prevoiusStepX = X; 
                _prevoiusStepY = Y;
            }

            return _result;
        }

        public override bool TryStep(IBaseCharacter character)
        {
           
            if (character is Hero)
            {
                character.Money -= Money;
                return true;
            }
            character.Hp -= Hp;
            return false;
        }
    }
}
