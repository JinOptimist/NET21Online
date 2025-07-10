using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsole.Maze.Cells.Inventory
{
    /// <summary>
    /// Class for items that can be in inventory
    /// </summary>
    public abstract class BaseItems : BaseCell
    {
        protected BaseItems(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {

        }

        public bool CanGet(Hero hero) => hero.SizeInventory >= hero.Inventory.Count + 1;
    }
}
