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
                    var npc = maze.Npcs.FirstOrDefault(cell => cell.X == x && cell.Y == y);
                    if (maze.Hero.X == x && maze.Hero.Y == y)
                    {
                        Console.Write(maze.Hero.Symbol);
                    }
                    else if (npc != null)
                    {
                        Console.Write(npc.Symbol);
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

            WriteInventoryNames(maze.Hero);
        }

        public void DarwMultiplayer(MazeMap maze, Hero heroNow)
        {
            Console.Clear();

            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    var npc = maze.Npcs.FirstOrDefault(cell => cell.X == x && cell.Y == y);
                    var hero = maze.Heroes.FirstOrDefault(h => h.X == x && h.Y == y);
                    if (hero != null && hero.Hp > 0)
                    {
                        if(hero == heroNow)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }

                        Console.Write(hero.Symbol);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (npc != null)
                    {
                        Console.Write(npc.Symbol);
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


            //States heroes - multiplayer
            for (int i = 0; i < maze.Heroes.Count; i++)
            {
                var hero = maze.Heroes[i];

                if (hero == heroNow)
                {
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine($"Hero{i + 1} = Money: {hero.Money}\tHp: {hero.Hp}");
                    Console.WriteLine($"Inventory [0-{hero.SizeInventory}]:");
                    WriteInventoryNames(hero);


                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine($"Hero{i + 1} = Money: {hero.Money}\tHp: {hero.Hp}");
                    Console.WriteLine($"Inventory [0-{hero.SizeInventory}]:");
                    WriteInventoryNames(hero);
                }
               
            }

            
        }

        public void GameOver(MazeMap maze)
        {
            Console.Clear();

            Console.WriteLine("All heroes died");
            for (int i = 0; i < maze.Heroes.Count; i++)
            {
                Console.WriteLine($"Hero{i + 1} = Money: {maze.Heroes[i].Money}");
            }
        }

        private void WriteInventoryNames(Hero hero)
        {
            var listInventoryNames = hero.GetInventoryNames();

            for (int i = 0; i < listInventoryNames.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {listInventoryNames[i]}");
            }

            Console.WriteLine();
        }
    }
}
