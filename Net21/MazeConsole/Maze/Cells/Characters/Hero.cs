using MazeConsole.Maze.Cells.Inventory;

namespace MazeConsole.Maze.Cells.Сharacters
{
    public class Hero : BaseCharacter, IHero
    {
        public int SizeInventory { get; set; } = 10;
        public List<BaseItems> Inventory { get; set; } = new List<BaseItems>();

        public Hero(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {

        }

        public override string Symbol => "@";

        public override bool TryStep(IBaseCharacter character)
        {
            character.Hp--;
            Hp--;

            return false;
        }

        public List<string> GetInventoryNames() => Inventory.Select(item => item.Name).ToList();

        public bool CanGet() => SizeInventory >= Inventory.Count + 1;

    }
}
