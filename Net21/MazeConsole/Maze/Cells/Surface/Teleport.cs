using MazeConsole.Maze.Cells.Сharacters;

namespace MazeConsole.Maze.Cells.Surface;

/// <summary>
/// Teleport player to another teleport;
/// Usage: make teleport as usual cell, then bind it to another(you can bind teleports to each other), when player step on teleport cell he will be teleported to cell which you marked in bind method;
/// (if you use not binded cell, it will raise exeption)
/// </summary>
public class Teleport : BaseCell
{
    // как бороться с
    // Non-nullable property 'EndPoint' must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring the property as nullable.
    // ?
    public Teleport(int x, int y, IMazeMap mazeMap) : base(x, y, mazeMap)
    {
        mazeMap.ReplaceCell(this);
    }

    public override string Symbol => "&";

    public Teleport EndPoint { get; set; }
    public bool IsBound { get; set; }


    public override bool TryStep(IBaseCharacter character)
    {
        if (IsBound)
        {
            character.X = EndPoint.X;
            character.Y = EndPoint.Y;
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