using MazeConsole.Maze;
using MazeConsole.Maze.Cells;
using System.Security.Cryptography;

namespace MazeConsole.Draw
{
    /// <summary>
    /// Provides functionality to render a visual representation of a maze.
    /// </summary>
    /// <remarks>This class is responsible for drawing a maze based on the provided <see cref="MazeMap"/>
    /// object. The specific rendering logic is determined by the implementation of the <see cref="Darw"/>
    /// method.</remarks>
    public class Drawer
    {
        /// <summary>
        /// Draw in console
        /// </summary>
        /// <param name="maze">Maze from which we get cells</param>
        public void Darw(MazeMap maze)
        {
            Console.Clear();

            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    if (maze.Hero.X == x && maze.Hero.Y == y)
                    {
                        Console.Write(maze.Hero.Symbol);
                    }
                    else
                    {
                        var cell = maze
                           .CellsSurface
                           .First(cell => cell.X == x && cell.Y == y);
                        Console.Write(cell.Symbol);
                    }
                }
                Console.WriteLine();
            }

            var hero = maze.Hero;
            Console.WriteLine($"Money: {hero.Money}\tHp: {hero.Hp}");
            Console.WriteLine($"Inventory [0-{hero.SizeInventory}]:");

            WriteInventoryNames(maze);

        }

        private void WriteInventoryNames(MazeMap maze)
        {
            var listInventoryNames = maze.Hero.GetInventoryNames();

            for (int i = 0; i < listInventoryNames.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {listInventoryNames[i]}");
            }
        }
    }
}
