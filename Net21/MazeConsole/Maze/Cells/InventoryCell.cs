using MazeConsole.Maze.PlayerExtention;

namespace MazeConsole.Maze.Cells;

/// <summary>
/// a cell that stores an Inventory Object that the player can pick up
/// Usage: is placed like a normal cell, but it is necessary to pass the InventoryObject that it stores
/// </summary>
public class InventoryCell:BaseCell
{
    public InventoryCell(int x, int y, MazeMap mazeMap, InventoryObject io) : base(x, y, mazeMap)
    {
        mazeMap.ReplaceCell(this);
        Item = io;
    }
    

    public override string Symbol => Item.Symbol;

    public InventoryObject Item { get; }


    public override bool TryStep(Hero hero)
    {
        if (hero.X != hero.PastX | hero.Y != hero.PastY)
        {
            hero.Inventory.AddInventoryObject(Item);
            MazeMap.ReplaceCell(new Ground(X, Y, MazeMap));
        }
        return true;
    }
}