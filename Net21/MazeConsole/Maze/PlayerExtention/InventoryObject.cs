namespace MazeConsole.Maze.PlayerExtention;


/// <summary>
/// unique type for storing game items
/// </summary>
public class InventoryObject
{
    public InventoryObject(string name, InventoryObjectType type, string symbol, string description)
    {
        Name = name;
        Type = type;
        Symbol = symbol;
        Description = description;
    }
    
    
    public string Name { get; }
    public InventoryObjectType Type { get; }
    public string Symbol { get; }
    public string Description { get; }

    public bool IsDressed = false;
    

    public override string ToString()
    {
        return $"[{Name}:<{Type}>]";
    }
}