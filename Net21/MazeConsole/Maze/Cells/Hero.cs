using MazeConsole.Maze.PlayerExtention;


namespace MazeConsole.Maze.Cells
{
    public class Hero : BaseCell
    {
        public int Money { get; set; }
        
        private int _hp;
        public int Hp
        {
            get
            {
                return _hp;
            }
            set
            {
                _hp = value;
                if (_hp <= 0)
                {
                    Dead();
                }
        }
        }
        
        public int PastX { get; set; }
        public int PastY { get; set; }

        public Inventory Inventory { get; }

        public Hero(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
            Inventory = new Inventory();
        }

        public override string Symbol => "@";

        public override bool TryStep(Hero hero)
        {
            throw new NotImplementedException();
        }

        public void ManageInventory()
        {
            PastX = X;
            PastY = Y;
            
            int pointer = 0;
            var isContinue = true;

            while (isContinue)
            {
                Console.Clear();
                Console.WriteLine("=========== Inventory manage ===========");
                Console.WriteLine(Inventory.Count > 0?Inventory.GetInventory()[pointer].Description : "No items");
                Console.WriteLine("========= Use arrows to locate =========");
                Console.WriteLine("====== press enter to manage item ======");
                Console.WriteLine("======== Press I or Tab to exit ========");
                
                for (int i = 0; i < Inventory.Count; i++)
                {
                    Console.Write(Inventory.GetInventory()[i]);
                    if (i == pointer)
                    {
                        Console.WriteLine(" <--");
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }

                Console.WriteLine("========================================");

                ConsoleKeyInfo userInput = Console.ReadKey();
                switch (userInput.Key)
                {
                    case ConsoleKey.I:
                    case ConsoleKey.Tab:
                        isContinue = false;
                        break;
                    case ConsoleKey.UpArrow:
                        pointer--;
                        break;
                    case ConsoleKey.DownArrow:
                        pointer++;
                        break;
                    case ConsoleKey.Enter:
                        ManageItem(Inventory.GetInventory()[pointer]);
                        break;
                }

                if (pointer < 0)
                {
                    pointer = Inventory.Count - 1;
                }
                else if (pointer > Inventory.Count - 1)
                {
                    pointer = 0;
                }
            }
        }

        private void ManageItem(InventoryObject item)
        {
            int pointer = 0;
            var isContinue = true;

            var menu = new Dictionary<string, Action<InventoryObject>>();

            if (MazeMap[X, Y] is Ground)
            {
                menu.Add("Drop", Drop);
            }

            if (item.Type == InventoryObjectType.Cloth)
            {
                if (item.IsDressed)
                {
                    menu.Add("Dress off", DressOff);
                }
                else
                {
                    menu.Add("Dress on", DressOn);
                }
            }
            menu.Add("Back", Console.WriteLine);

            while (isContinue)
            {
                Console.Clear();
                Console.WriteLine("============= Item manager =============");
                Console.WriteLine(item.Description);
                Console.WriteLine("========= Use arrows to locate =========");
                Console.WriteLine("====== press enter to make action ======");
                
                for (int i = 0; i < menu.Count; i++)
                {
                    Console.Write(menu.ElementAt(i).Key);
                    if (i == pointer)
                    {
                        Console.WriteLine(" <--");
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }

                Console.WriteLine("========================================");

                ConsoleKeyInfo userInput = Console.ReadKey();
                switch (userInput.Key)
                {
                    case ConsoleKey.UpArrow:
                        pointer--;
                        break;
                    case ConsoleKey.DownArrow:
                        pointer++;
                        break;
                    case ConsoleKey.Enter:
                        menu.ElementAt(pointer).Value.Invoke(item);
                        isContinue = false;
                        break;
                }

                if (pointer < 0)
                {
                    pointer = menu.Count - 1;
                }
                else if (pointer > menu.Count - 1)
                {
                    pointer = 0;
                }
            }
        }

        private void Drop(InventoryObject io)
        {
            DressOff(io);
            Inventory.DelInventoryObject(io);
            MazeMap.ReplaceCell(new InventoryCell(X, Y, MazeMap, io)); //problem
        }

        private void DressOn(InventoryObject io)
        {
            io.IsDressed = true;
        }
        
        private void DressOff(InventoryObject io)
        {
            io.IsDressed = false;
        }
        

        public void Dead()
        {
            Console.Clear();
            Console.WriteLine("|   YOU ARE DEAD   |");
            Environment.Exit(0);
        }
    }
}
