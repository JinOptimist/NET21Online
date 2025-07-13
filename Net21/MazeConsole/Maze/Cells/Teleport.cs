using MazeConsole.Maze.CellsInterfaces;


namespace MazeConsole.Maze.Cells;

/// <summary>
/// Teleport player to another teleport;
/// Usage: make teleport as usual cell, then bind it to another(you can bind teleports to each other), when player step on teleport cell he will be teleported to cell which you marked in bind method;
/// (if you use not binded cell, it will raise exeption)
/// </summary>
public class Teleport : BaseCell, IBind<Teleport>
{
    // как бороться с
    // Non-nullable property 'EndPoint' must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring the property as nullable.
    // ?
    public Teleport(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
    {
        mazeMap.ReplaceCell(this);
    }

    public override string Symbol => "&";

    public Teleport EndPoint { get; set; }
    public bool IsBound { get; set; }


    public override bool TryStep(Hero hero)
    {
        if (IsBound)
        {
            if (hero.Inventory.CheckOnDress("Unstable crystal"))
            {
                var rand = new Random();
                hero.X = rand.Next(MazeMap.Width);
                hero.Y = rand.Next(MazeMap.Height);
                if (!MazeMap[hero.X, hero.Y].TryStep(hero))
                {
                    hero.Dead();
                }
            }
            else
            {
                hero.X = EndPoint.X;
                hero.Y = EndPoint.Y;
            }
        }
        else
        {
            throw new Exception("Teleport wasn't bind");
        }

        return false;
    }
    
    public void Bind(Teleport endPoint)
    {
        EndPoint = endPoint;
        IsBound = true;
    }
}