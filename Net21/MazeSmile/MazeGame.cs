using MazeSmile.MazeData.Cells;
using MazeSmile.MazeDrawer;

namespace MazeSmile
{
	public class MazeGame
	{
		private Maze _maze;
		private Player _player;

		public MazeGame(int width, int height)
		{
			_maze = new Maze(width, height);
			_player = new Player(1, 1); // Start inside the maze
		}

		public void Start()
		{
            
			while (true)
			{
				Console.Clear();
                Drawer.DrawMaze(_maze, _player);
                Console.WriteLine("Use WASD to move. Q to quit.");
				var pressedKey = Console.ReadKey(true).Key;
				if (pressedKey == ConsoleKey.Q)
				{
					break;
				}
				int proposedX = _player.X;
				int proposedY = _player.Y;
				switch (pressedKey)
				{
					case ConsoleKey.W:
						{
							proposedY--;
							break;
						}
					case ConsoleKey.S:
						{
							proposedY++;
							break;
						}
					case ConsoleKey.A:
						{
							proposedX--;
							break;
						}
					case ConsoleKey.D:
						{
							proposedX++;
							break;
						}
				}
				if (_maze.IsInside(proposedX, proposedY) && _maze.Cells[proposedX, proposedY].CanStep)
				{
					_player.X = proposedX;
					_player.Y = proposedY;
					_maze.Cells[proposedX, proposedY].OnStep(_player);
				}
			}
		}
	}
}
