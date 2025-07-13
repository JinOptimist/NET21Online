using MazeConsole.Maze.PlayerExtention;
using MazeConsole.Maze.CellsInterfaces;


namespace MazeConsole.Maze.Cells;

/// <summary>
/// door;
/// Usage: is placed on the map and controls by other objects with help of IActive
/// </summary>
public class Door : BaseCell, IActive
{
    // как бороться с
    // Non-nullable property 'Key' must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring the property as nullable.
    // ?
    
    // BaseCell
    public Door(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
    {
        mazeMap.ReplaceCell(this);
        IsWin = false;
        IsActive = false;
    }

    public Door(int x, int y, MazeMap mazeMap, InventoryObject key) : this(x, y, mazeMap)
    {
        if (key.Type == InventoryObjectType.Key)
        {
            Key = key;
        }
        else
        {
            throw new Exception($"Was expecting a 'Key' type, but was gotten '{key.Type}'");
        }
    }
    
    public Door(int x, int y, MazeMap mazeMap, int coast) : this(x, y, mazeMap)
    {
        Coast = coast;
    }
    

    public override string Symbol => GetSymbol();

    private InventoryObject Key { get; }
    private int Coast { get; } = int.MaxValue;
    public bool IsWin { get; set; }


    public string GetSymbol()
    {
        switch (IsActive)
        {
            case true:
                return " ";
            case false:
                return "D";
        }
    }

    public override bool TryStep(Hero hero)
    {
        if (hero.Inventory.HaveInventoryObject(Key))
        {
            hero.Inventory.DelInventoryObject(Key);
            IsActive = true;
        }
        else if (hero.Money >= Coast)
        {
            hero.Money -= Coast;
            IsActive = true;
        }
        if (IsWin & IsActive)
        {
            Console.Clear();
            Console.WriteLine("|   YOU ARE WIN   |");
            Environment.Exit(0);
            Console.ReadKey();
        }
        
        return IsActive;
    }
    
    
    public bool IsActive { get; set; }
}