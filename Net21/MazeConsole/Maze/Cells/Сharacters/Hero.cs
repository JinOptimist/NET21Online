using MazeConsole.Maze.Cells.Inventory;
using MazeConsole.Maze.Cells.Сharacters.Npcs;
using MazeConsole.Maze.Cells.Сharacters.Pets;
using static MazeConsole.Maze.Cells.Сharacters.Npcs.Relation;

namespace MazeConsole.Maze.Cells.Сharacters
{
    public class Hero : BaseCharacter
    {
        public int SizeInventory { get; set; } = 10;
        public List<BaseItems> Inventory { get; set; } = new List<BaseItems>();

        public Hero(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
            Damage = 1;
            Money = 0;
            Hp = 10;
        }

        public override string Symbol => "@";

        public override bool TryStep(BaseCharacter character)
        {
            if (character.Relation is RelationType.Friend)
            {
                return false;
            }

            var nearCells = MazeMap.GetNearCell(this);
            var pet = nearCells.OfType<BasePet>().FirstOrDefault();

            if (pet != null)
            {
                character.Hp = character.Hp - (Damage + pet.Damage);
            }
            else
            {
                character.Hp -= Damage;
            }

            Hp -= character.Damage;

            return false;
        }

        public List<string> GetInventoryNames() => Inventory.Select(item => item.Name).ToList();

        public bool CanGet() => SizeInventory >= Inventory.Count + 1;

    }
}
