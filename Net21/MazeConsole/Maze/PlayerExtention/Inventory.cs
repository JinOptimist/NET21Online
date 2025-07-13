namespace MazeConsole.Maze.PlayerExtention;


/// <summary>
/// Provides item storage for the player
/// </summary>
public class Inventory
{
    // как бороться с 
    // Redundant type specification
    // ?
    private readonly List<InventoryObject> _inventory = new List<InventoryObject>();

    public int Count => _inventory.Count;
    
    public void AddInventoryObject(InventoryObject io)
    {
        _inventory.Add(io);
    }

    public void DelInventoryObject(string name)
    {
        var io = GetInventoryObject(name);
        if (io != null)
        {
            _inventory.Remove(io);
        }
    }
    
    public void DelInventoryObject(InventoryObject io)
    {
        var item = GetInventoryObject(io);
        if (item != null)
        {
            _inventory.Remove(item);
        }
    }

    public InventoryObject? GetInventoryObject(string name)
    {
        return _inventory.FirstOrDefault(x => x.Name == name);
    }
    
    public InventoryObject? GetInventoryObject(InventoryObject io)
    {
        return _inventory.FirstOrDefault(x => x == io);
    }

    public bool HaveInventoryObject(InventoryObject io)
    {
        return _inventory.Contains(io);
    }
    
    public bool HaveInventoryObject(string name)
    {
        return (GetInventoryObject(name) != null);
    }

    public bool CheckOnDress(string name)
    {
        var item = _inventory.Find(x => x.Name == name);
        if (item != null)
        {
            return item.IsDressed;
        }

        return false;
    }

    public List<InventoryObject> GetInventory()
    {
        return _inventory;
    }
    
    public override string ToString()
    {
        return string.Join(", ", _inventory);
    }

}