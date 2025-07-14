using MazeConsole.Maze.Cells.Inventory;

namespace MazeConsole.Maze.Cells.Сharacters
{
    public class Hero : BaseCharacter
    {
        public int SizeInventory { get; set; } = 10;
        public List<BaseItems> Inventory { get; set; } = new List<BaseItems>();

        public Hero(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
            Inventory = new List<BaseItems>(SizeInventory);
        }

        public override string Symbol => "@";

        public override bool TryStep(BaseCharacter character)
        {
            character.Hp--;
            Hp--;

            return false;
        }

        public List<string> GetInventoryNames()
        {
            List<string> listInventoryNames = new List<string>();

            foreach (var item in Inventory)
            {
                listInventoryNames.Add(item.GetType().Name);
            }

            return listInventoryNames;
        }
    }
}
