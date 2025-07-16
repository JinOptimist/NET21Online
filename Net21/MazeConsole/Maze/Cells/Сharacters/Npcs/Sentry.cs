using MazeConsole.Maze.Cells.Surface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsole.Maze.Cells.Сharacters.Npcs
{
    public class Sentry : BaseNpc
    {
        private int _prevoiusStepX;
        private int _prevoiusStepY;

        public Sentry(int x, int y, MazeMap mazeMap, int hp, int money) : base(x, y, mazeMap, hp, money)
        {
            Hp = hp;
            Money = money;
            _prevoiusStepX = x;
            _prevoiusStepY = y;
        }

        public override string Symbol => "?";

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
            

            if (bottomGround != null)
            {
                return bottomGround;
            }

            if (topGround != null)
            {
                return topGround;
            }

            if (leftGround != null)
            {
                return leftGround;
            }

            return rightGround;

            }

        public override bool TryStep(BaseCharacter character)
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
