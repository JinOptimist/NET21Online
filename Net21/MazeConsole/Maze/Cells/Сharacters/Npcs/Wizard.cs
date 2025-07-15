using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsole.Maze.Cells.Сharacters.Npcs
{
    public class Wizard : BaseNpc
    {
        public Wizard(int x, int y, MazeMap mazeMap, bool isGoodMood, int hp = 10) : base(x, y, mazeMap)
        {
            Hp = hp;
            IsGoodMood = isGoodMood;
        }
        public bool IsGoodMood { get; set; }
        
        public override string Symbol => "?";

        public override BaseCell? CellToMove()
        {
            var nearCells = MazeMap.GetNearCell(this).Where(x => x is not Wall);
            var hero = nearCells.OfType<Hero>().FirstOrDefault();
            if (hero != null) 
            {
                return hero;
            }
            return nearCells.First();
        }

        public override bool TryStep(BaseCharacter character)
        {
            if(IsGoodMood)
            {
                character.Hp += character.Hp;
                this.Hp = 0;
                return true;
            }
            character.Hp = 1;
            this.Hp = 0;
            return true;
        }
    }
}
