﻿using MazeCore.Maze;
using MazeCore.Maze.Cells;
using MazeCore.Maze.Cells.Characters;
using MazeCore.Maze.Cells.Surface;


namespace MazeCore.Maze.Cells.Characters.Npcs
{
    public class Wizard : BaseNpc
    {
        public Wizard(int x, int y, IMazeMap mazeMap, bool isGoodMood, int hp = 10) : base(x, y, mazeMap)
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

        public override bool TryStep(IBaseCharacter character)
        {
            if (IsGoodMood)
            {
                character.Hp += character.Hp;
                Hp = 0;
                return true;
            }
            character.Hp = 1;
            Hp = 0;
            return true;
        }
    }
}
