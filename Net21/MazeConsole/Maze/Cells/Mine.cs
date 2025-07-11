using MazeConsole.Maze.Cells;
using MazeConsole.Maze;

/// <summary>
/// Represents a mine cell in the maze that damages the hero and periodically moves.
/// </summary>
public class Mine : BaseCell
{
    private static readonly Random _random = new Random();
    private DateTime _lastMoveTime = DateTime.Now;

    public Mine(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
    {
    }

    public override string Symbol => "x";

    public override bool TryStep(Hero hero)
    {
        hero.Hp--;
       
        return true; 
    }

    public void TryMove(MazeMap maze)
    {
        if ((DateTime.Now - _lastMoveTime).TotalSeconds >= 5)
        {
            _lastMoveTime = DateTime.Now;

            var availableGrounds = maze.CellsSurface
                .OfType<Ground>()
                .Where(g => g.X != maze.Hero.X || g.Y != maze.Hero.Y)
                .ToList();

            if (availableGrounds.Count > 0)
            {
                var newPos = availableGrounds[_random.Next(availableGrounds.Count)];

                // Перемещаем мину
                maze.ReplaceCell(new Ground(X, Y, maze));
                maze.ReplaceCell(new Mine(newPos.X, newPos.Y, maze));
            }
        }
    }
}
