using MazeConsole.Maze.Cells.Inventory;
using MazeCore.Maze;
using MazeCore.Maze.Cells.Items;

namespace MazeCore.Maze.Cells.Characters
{
    public class Hero : BaseCharacter, IHero
    {
        public int SizeInventory { get; set; } = 10;
        public List<IBaseItems> Inventory { get; set; } = new List<IBaseItems>();

        public Hero(int x, int y, IMazeMap mazeMap) : base(x, y, mazeMap)
        {

        }

        public Hero(int x, int y, IMazeMap mazeMap, int hp, int money) : base(x, y, mazeMap)
        {
            Hp = hp;
            Money = money;
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

        public bool isAlife => Hp != 0;

    }
}
