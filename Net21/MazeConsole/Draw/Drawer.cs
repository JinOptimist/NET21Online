using MazeConsole.Maze;
using MazeConsole.Maze.Cells.Сharacters;

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
                        ///Закоменчено в связи с тем что при добавлении морских монстров и мин
                        ///карта начала почему то вниз лететь  
                        ///метод при этом не меняется сам в изменениях и работает стабильно если 
                        ///есть предложения готов выслушать
                        
                        //var cell = maze
                        //   .CellsSurface
                        //   .First(cell => cell.X == x && cell.Y == y);
                        //Console.Write(cell.Symbol);
                        var cell = maze[x, y];  

                        if (cell != null)
                        {
                            Console.Write(cell.Symbol);
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                }
                Console.WriteLine();
            }

            var hero = maze.Hero;
            Console.WriteLine($"Money: {hero.Money}\tHp: {hero.Hp}");
            Console.WriteLine($"Inventory [0-{hero.SizeInventory}]:");

            WriteInventoryNames(maze.Hero);

        }

        private void WriteInventoryNames(Hero hero)
        {
            var listInventoryNames = hero.GetInventoryNames();

            for (int i = 0; i < listInventoryNames.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {listInventoryNames[i]}");
            }
        }
    }
}
