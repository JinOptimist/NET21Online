using MazeCore.Maze;
using MazeCore.Maze.Cells.Characters;
using System.Text;

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
            var output = new StringBuilder();

            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    var npc = maze.Npcs.FirstOrDefault(cell => cell.X == x && cell.Y == y);
                    if (maze.Hero.X == x && maze.Hero.Y == y)
                    {
                        output.Append(maze.Hero.Symbol);
                    }
                    else if (npc != null)
                    {
                        output.Append(npc.Symbol);
                    }
                    else
                    {
                        var cell = maze
                           .CellsSurface
                           .First(cell => cell.X == x && cell.Y == y);
                        output.Append(cell.Symbol);
                    }
                }
                output.AppendLine();
            }

            var hero = maze.Hero;
            output.AppendLine($"Money: {hero.Money}\tHp: {hero.Hp}");
            output.AppendLine($"Inventory [0-{hero.SizeInventory}]:");

            var listInventoryNames = hero.GetInventoryNames();
            for (int i = 0; i < listInventoryNames.Count; i++)
            {
                output.AppendLine($"{i + 1}) {listInventoryNames[i]}");
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(output.ToString());

            int linesToClear = Console.WindowHeight - (maze.Height + 3 + listInventoryNames.Count);
            if (linesToClear > 0)
            {
                string clearLine = new string(' ', Console.WindowWidth);
                for (int i = 0; i < linesToClear; i++)
                {
                    Console.WriteLine(clearLine);
                }
            }
        }
    }
}
