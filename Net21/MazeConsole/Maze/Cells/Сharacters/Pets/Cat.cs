using MazeConsole.Maze.Cells.Surface;
using MazeConsole.Maze.Cells.Сharacters.Npcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MazeConsole.Maze.Cells.Сharacters.Npcs.Relation;

namespace MazeConsole.Maze.Cells.Сharacters.Pets
{
    public class Cat : BasePet
    {
        /// <summary>
        /// The cat is a pet that follows the player only. It cannot attack, but only improves the Hero's stats.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="mazeMap"></param>
        /// <param name="hp"></param>
        /// <param name="maney"></param>
        public Cat(int x, int y, MazeMap mazeMap, int hp, int damageBaf, int maney) : base(x, y, mazeMap, hp, damageBaf, maney)
        {
            Hp = hp;
            Money = maney;
            Damage = damageBaf;
            Relation = RelationType.Friend;
        }

        public override string Symbol => "C";

        private bool _findHero;

        private (int x, int y) _lastCoordinatesHero;

        /// <summary>
        /// We start moving if the Cat finds the Hero
        /// </summary>
        /// <param name="hero"></param>
        /// <returns></returns>
        public override BaseCell? CellToMove(Hero hero)
        {
            var nearCells = MazeMap.GetNearCell(this);

            if (_findHero && (_lastCoordinatesHero.y != hero.Y || _lastCoordinatesHero.x != hero.X))
            {
                var lastX = _lastCoordinatesHero.x;
                var lastY = _lastCoordinatesHero.y;
                _lastCoordinatesHero = (hero.X, hero.Y);

                return MazeMap.CellsSurface.FirstOrDefault(c => c.X == lastX && c.Y == lastY);
            }
            
            if (nearCells.Any(c => c is Hero))
            {
                _findHero = true;
                _lastCoordinatesHero = (hero.X, hero.Y);
                return null;
            }

            return null;
        }

        public override bool TryStep(BaseCharacter character)
        {
            if (character is not Hero)
            {
                Hp -= character.Damage;
                return false;
            }

            return false;
        }

    }
}
