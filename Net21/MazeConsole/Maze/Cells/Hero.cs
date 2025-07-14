using MazeConsole.Maze.Cells.Inventory;

namespace MazeConsole.Maze.Cells
{
    public class Hero : BaseCell
    {
        public int Money { get; set; }
        public int Hp { get; set; }

        public int SizeInventory { get; set; } = 10;
        public List<BaseItems> Inventory { get; set; } = new List<BaseItems>();

        public Hero(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
            Inventory = new List<BaseItems>(SizeInventory);
        }

        public override string Symbol => "@";

        public override bool TryStep(Hero hero)
        {
            throw new NotImplementedException();
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
