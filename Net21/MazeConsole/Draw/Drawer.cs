using MazeCore.Maze;
using MazeCore.Maze.Cells.Characters;
using MazeCore.Maze.Cells.Characters.Npcs;
using MazeCore.Maze.Cells.Surface;


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
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(maze.Hero.Symbol);
                        Console.ResetColor();
                    }
                    else if (npc != null)
                    {
                        if (npc is Cultist)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        
                        else if (npc is Dragon)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                        }
                        
                        else if (npc is EvilSpirit)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        
                        else if (npc is Goblin)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        
                        else if (npc is Sentry)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }
                        
                        else if (npc is Snow)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                        }
                        
                        else if (npc is Thief)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        
                        else if (npc is Wizard)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        
                        else if (npc is Wolf)
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        
                        Console.Write(npc.Symbol);
                        Console.ResetColor();
                    }
                    
                    else
                    {
                        var cell = maze
                           .CellsSurface
                           .First(cell => cell.X == x && cell.Y == y);

                        if (cell is Coin)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        
                        else if (cell is FirstAidKit)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        
                        else if (cell is Ground)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                        }
                        
                        else if (cell is HealingWell) 
                        { 
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        
                        else if (cell is Ice)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        
                        else if (cell is Lava)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        
                        else if (cell is Return)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        
                        else if (cell is Sea)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                        }
                        
                        else if (cell is Sea)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                        }
                        
                        else if (cell is Snake)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                        }
                        
                        else if (cell is Teleport)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                        }
                        
                        else if (cell is Trap)
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        
                        else if (cell is Wall)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                        }
                        
                        Console.Write(cell.Symbol);
                        Console.ResetColor();
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
