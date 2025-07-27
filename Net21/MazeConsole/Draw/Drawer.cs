using MazeCore.Maze;
using MazeCore.Maze.Cells.Characters;

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
        private Dictionary<(int x, int y), char> _lastFrame = new();
        /// <summary>
        /// Draw in console
        /// </summary>
        /// <param name="maze">Maze from which we get cells</param>
        public void Darw(MazeMap maze)
        {
            //Console.Clear();

            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    char symbol;
                    if (maze.Hero.X == x && maze.Hero.Y == y)
                    {
                        symbol = maze.Hero.Symbol[0];
                    }
                    else
                    {
                        var npc = maze.Npcs.FirstOrDefault(cell => cell.X == x && cell.Y == y);
                        if (npc != null)
                        {
                            symbol = npc.Symbol[0];
                        }
                        else
                        {
                            var cell = maze
                               .CellsSurface
                               .First(cell => cell.X == x && cell.Y == y);
                            symbol = cell.Symbol[0];
                        }
                    }
                    WriteIfChanged(x, y, symbol);

                }
            }

            var hero = maze.Hero;
            int baseY = maze.Height + 1;
            
            ClearLine(baseY);
            Console.SetCursorPosition(0, baseY);
            Console.WriteLine($"Money: {hero.Money}\tHp: {hero.Hp}");
            Console.WriteLine($"Inventory [0-{hero.SizeInventory}]:");
            WriteInventoryNames(maze.Hero);

            var messages = maze.Messages?.TakeLast(3).ToList() ?? new();
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(0, maze.Height + 5 + i);
                if (i < messages.Count)
                    Console.WriteLine(messages[i].PadRight(Console.WindowWidth));
                else
                    Console.WriteLine(new string(' ', Console.WindowWidth)); 
            }

        }

        private void WriteIfChanged(int x, int y, char symbol)
        {
            if (!_lastFrame.TryGetValue((x, y), out char prevSymbol) || prevSymbol != symbol)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(symbol);
                _lastFrame[(x, y)] = symbol;
            }
        }

        private void WriteInventoryNames(Hero hero)
        {
            var listInventoryNames = hero.GetInventoryNames();

            for (int i = 0; i < listInventoryNames.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {listInventoryNames[i]}");
            }
        }
        private void ClearLine(int y)
        {
            Console.SetCursorPosition(0, y);
            Console.Write(new string(' ', Console.WindowWidth));
        }
    }
}
