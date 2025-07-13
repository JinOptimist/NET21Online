using MazeConsole.Maze.CellsInterfaces;


namespace MazeConsole.Maze.Cells;

/// <summary>
/// pressure plate;
/// Usage: is placed on the map and is bound to an object implementing IActive
/// </summary>
public class Button: BaseCell, IActive, IBind<IActive>
{
    // как бороться с
    // "Non-nullable property 'EndPoint' must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring the property as nullable."
    // ?
    public Button(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
    {
        mazeMap.ReplaceCell(this);
        IsBound = false;
        IsActive = false;
    }

    
    public override string Symbol => GetSymbol();

    public override bool TryStep(Hero hero)
    {
        if (IsBound)
        {
            IsActive = true;
            EndPoint.IsActive = true;
        }
        else
        {
            throw new Exception("Button wasn't bind");
        }

        return true;
    }


    public bool IsActive { get; set; }

    public string GetSymbol()
    {
        switch (IsActive)
        {
            case true:
                return "_";
            case false:
                return "-";
        }
    }
    
    
    public IActive EndPoint { get; set; }
    public bool IsBound { get; set; }

    public void Bind(IActive endPoint)
    {
        EndPoint = endPoint;
        IsBound = true;
    }
}