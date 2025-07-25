﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeCore.Maze;
using MazeCore.Maze.Cells;
using MazeCore.Maze.Cells.Characters;
using MazeCore.Maze.Cells.Surface;

namespace MazeCore.Maze.Cells.Characters.Npcs
{
    public class Thief : BaseNpc
    {
        public int StolenMoney { get; set; }
        public Thief(int x, int y, IMazeMap mazeMap, int hp = 6) : base(x, y, mazeMap)
        {
            Hp = hp;
        }

        public override string Symbol => "T";

        public override BaseCell? CellToMove()
        {
            var neraCells = MazeMap
                .GetNearCell(this);
            var hero = neraCells.OfType<Hero>().FirstOrDefault();
            if (hero != null)
            {
                return hero;
            }

            var grounds = MazeMap
                .GetNearCell(this)
                .OfType<Ground>();
            if (!grounds.Any())
            {
                return null;
            }

            return grounds.First();
        }

        public override bool TryStep(IBaseCharacter character)
        {
            if (character is IHero hero)
            {
                StolenMoney = hero.Money;
                hero.Money = 0;
                return true;
            }
            return false;
        }
    }
}
