using MazeConsole.Maze;
using MazeConsole.Maze.Cells;
using MazeConsole.Maze.PlayerExtention;


namespace MazeConsole.Builder
{
    public class MazeBuilder
    {
        private MazeMap _currentSurface;

        public MazeMap BuildSurface(int width, int height)
        {
            _currentSurface = new MazeMap(width, height);

            BuildWall();
            BuildGround();
            BuildInnerWall();
            // BuildCoin();
            BuildSingleCoins();
            BuildIce();
            BuildTeleports();
            BuildLogicElements();

            BuildHero();

            return _currentSurface;
        }

        private void BuildCoin()
        {
            var cellToReplace = _currentSurface
                .CellsSurface
                .OfType<Ground>()
                .Where(cell => cell.X == _currentSurface.Width - 2
                               || cell.Y == _currentSurface.Height - 2)
                .ToList();
            foreach (var cell in cellToReplace)
            {
                var coin = new Coin(cell.X, cell.Y, _currentSurface);
            }
        }

        private void BuildSingleCoins()
        {
            new Coin(14, 16, _currentSurface);
            
            new Coin(17, 20, _currentSurface);
            new Coin(17, 22, _currentSurface);
            new Coin(17, 24, _currentSurface);
            
            new Coin(1, 18, _currentSurface);
            new Coin(1, 26, _currentSurface);
            new Coin(5, 22, _currentSurface);
            new Coin(9, 18, _currentSurface);
            new Coin(9, 26, _currentSurface);
            
            new Coin(5, 11, _currentSurface);
            
            new Coin(1, 11, _currentSurface);
            new Coin(2, 11, _currentSurface);
            new Coin(1, 12, _currentSurface);
            new Coin(2, 12, _currentSurface);
            new Coin(3, 12, _currentSurface);
            new Coin(1, 13, _currentSurface);
            new Coin(2, 13, _currentSurface);
            new Coin(3, 13, _currentSurface);
            new Coin(1, 14, _currentSurface);
            new Coin(2, 14, _currentSurface);
            new Coin(3, 14, _currentSurface);
            new Coin(1, 15, _currentSurface);
            new Coin(2, 15, _currentSurface);
            new Coin(3, 15, _currentSurface);
            new Coin(1, 16, _currentSurface);
            new Coin(2, 16, _currentSurface);
            new Coin(3, 16, _currentSurface);
            
            new Coin(28, 18, _currentSurface);
            new Coin(28, 19, _currentSurface);
        }

        private void BuildHero()
        {
            var hero = new Hero(20, 11, _currentSurface);
            hero.Hp = 10;
            hero.Money = 0;
            _currentSurface.Hero = hero;
        }

        private void BuildGround()
        {
            var cellWhichWeReplaceToGround = _currentSurface
                .CellsSurface
                .Where(cell => cell.X != 0 && cell.Y != 0
                                           && cell.X != _currentSurface.Width - 1
                                           && cell.Y != _currentSurface.Height - 1)
                .ToList();

            foreach (var cell in cellWhichWeReplaceToGround)
            {
                var ground = new Ground(cell.X, cell.Y, _currentSurface);
                _currentSurface.ReplaceCell(ground);
            }
        }

        private void BuildWall()
        {
            for (int y = 0; y < _currentSurface.Height; y++)
            {
                for (int x = 0; x < _currentSurface.Width; x++)
                {
                    var wall = new Wall(x, y, _currentSurface);
                    _currentSurface.CellsSurface.Add(wall);
                }
            }
        }

        private void BuildInnerWall()
        {
            //vertical
            new InnerWall(20, 1, _currentSurface);
            new InnerWall(20, 2, _currentSurface);
            new InnerWall(20, 3, _currentSurface);
            new InnerWall(20, 4, _currentSurface);
            
            
            new InnerWall(30, 1, _currentSurface);
            new InnerWall(30, 2, _currentSurface);
            // door
            new InnerWall(30, 4, _currentSurface);
            
            
            new FakeWall(4, 11, _currentSurface);
            new InnerWall(4, 12, _currentSurface);
            new InnerWall(4, 13, _currentSurface);
            new InnerWall(4, 14, _currentSurface);
            new InnerWall(4, 15, _currentSurface);
            new InnerWall(4, 16, _currentSurface);
            
            
            new InnerWall(10, 18, _currentSurface);
            new InnerWall(10, 19, _currentSurface);
            new InnerWall(10, 20, _currentSurface);
            new InnerWall(10, 21, _currentSurface);
            // door
            new InnerWall(10, 23, _currentSurface);
            new InnerWall(10, 24, _currentSurface);
            new InnerWall(10, 25, _currentSurface);
            new InnerWall(10, 26, _currentSurface);
            
            
            new InnerWall(20, 18, _currentSurface);
            new InnerWall(20, 19, _currentSurface);
            new InnerWall(20, 20, _currentSurface);
            new InnerWall(20, 21, _currentSurface);
            // door
            new InnerWall(20, 23, _currentSurface);
            new InnerWall(20, 24, _currentSurface);
            new InnerWall(20, 25, _currentSurface);
            new InnerWall(20, 26, _currentSurface);
            
            
            new InnerWall(27, 18, _currentSurface);
            new InnerWall(27, 19, _currentSurface);
            
            
            new InnerWall(27, 23, _currentSurface);
            new InnerWall(27, 24, _currentSurface);
            
            
            new InnerWall(30, 18, _currentSurface);
            new InnerWall(30, 19, _currentSurface);
            new InnerWall(30, 20, _currentSurface);
            new InnerWall(30, 21, _currentSurface);
            new InnerWall(30, 22, _currentSurface);
            // ground
            // ground
            new InnerWall(30, 25, _currentSurface);
            new InnerWall(30, 26, _currentSurface);
            
            
            // horizontal
            new InnerWall(1, 5, _currentSurface);
            new InnerWall(2, 5, _currentSurface);
            new InnerWall(3, 5, _currentSurface);
            new InnerWall(4, 5, _currentSurface);
            // door
            new InnerWall(6, 5, _currentSurface);
            new InnerWall(7, 5, _currentSurface);
            new InnerWall(8, 5, _currentSurface);
            new InnerWall(9, 5, _currentSurface);
            
            
            new InnerWall(1, 10, _currentSurface);
            new InnerWall(2, 10, _currentSurface);
            new InnerWall(3, 10, _currentSurface);
            new InnerWall(4, 10, _currentSurface);
            new InnerWall(5, 10, _currentSurface);
            new InnerWall(6, 10, _currentSurface);
            // door
            new InnerWall(8, 10, _currentSurface);
            new InnerWall(9, 10, _currentSurface);
            
            
            new InnerWall(31, 11, _currentSurface);
            new InnerWall(32, 11, _currentSurface);
            new InnerWall(33, 11, _currentSurface);
            new InnerWall(34, 11, _currentSurface);
            new InnerWall(35, 11, _currentSurface);
            new InnerWall(36, 11, _currentSurface);
            new InnerWall(37, 11, _currentSurface);
            new InnerWall(38, 11, _currentSurface);
            new InnerWall(39, 11, _currentSurface);
            new InnerWall(40, 11, _currentSurface);
            
            
            new InnerWall(1, 17, _currentSurface);
            new InnerWall(2, 17, _currentSurface);
            new InnerWall(3, 17, _currentSurface);
            new InnerWall(4, 17, _currentSurface);
            // door
            new InnerWall(6, 17, _currentSurface);
            new InnerWall(7, 17, _currentSurface);
            new InnerWall(8, 17, _currentSurface);
            new InnerWall(9, 17, _currentSurface);
            
            new InnerWall(27, 20, _currentSurface);
            new InnerWall(28, 20, _currentSurface);
            
            
            // box
            new InnerWall(10, 5, _currentSurface);
            new InnerWall(11, 5, _currentSurface);
            new InnerWall(12, 5, _currentSurface);
            new InnerWall(13, 5, _currentSurface);
            new InnerWall(14, 5, _currentSurface);
            new InnerWall(15, 5, _currentSurface);
            new InnerWall(16, 5, _currentSurface);
            new InnerWall(17, 5, _currentSurface);
            new InnerWall(18, 5, _currentSurface);
            new InnerWall(19, 5, _currentSurface);
            new InnerWall(20, 5, _currentSurface);
            new InnerWall(21, 5, _currentSurface);
            new InnerWall(22, 5, _currentSurface);
            new InnerWall(23, 5, _currentSurface);
            new InnerWall(24, 5, _currentSurface);
            new InnerWall(25, 5, _currentSurface);
            new InnerWall(26, 5, _currentSurface);
            new InnerWall(27, 5, _currentSurface);
            new InnerWall(28, 5, _currentSurface);
            new InnerWall(29, 5, _currentSurface);
            new InnerWall(30, 5, _currentSurface);
            
            
            new InnerWall(10, 17, _currentSurface);
            new InnerWall(11, 17, _currentSurface);
            new InnerWall(12, 17, _currentSurface);
            new InnerWall(13, 17, _currentSurface);
            // door
            new InnerWall(15, 17, _currentSurface);
            new InnerWall(16, 17, _currentSurface);
            new InnerWall(17, 17, _currentSurface);
            new InnerWall(18, 17, _currentSurface);
            new InnerWall(19, 17, _currentSurface);
            new InnerWall(20, 17, _currentSurface);
            new InnerWall(21, 17, _currentSurface);
            new InnerWall(22, 17, _currentSurface);
            new InnerWall(23, 17, _currentSurface);
            new InnerWall(24, 17, _currentSurface);
            new InnerWall(25, 17, _currentSurface);
            new InnerWall(26, 17, _currentSurface);
            new InnerWall(27, 17, _currentSurface);
            new InnerWall(28, 17, _currentSurface);
            new InnerWall(29, 17, _currentSurface);
            new InnerWall(30, 17, _currentSurface);
            
            
            new InnerWall(10, 6, _currentSurface);
            new InnerWall(10, 7, _currentSurface);
            new InnerWall(10, 8, _currentSurface);
            new InnerWall(10, 9, _currentSurface);
            new InnerWall(10, 10, _currentSurface);
            new InnerWall(10, 11, _currentSurface);
            new InnerWall(10, 12, _currentSurface);
            new InnerWall(10, 13, _currentSurface);
            new InnerWall(10, 14, _currentSurface);
            new InnerWall(10, 15, _currentSurface);
            new InnerWall(10, 16, _currentSurface);
            
            
            new InnerWall(30, 6, _currentSurface);
            new InnerWall(30, 7, _currentSurface);
            new InnerWall(30, 8, _currentSurface);
            new InnerWall(30, 9, _currentSurface);
            new InnerWall(30, 10, _currentSurface);
            new InnerWall(30, 11, _currentSurface);
            new InnerWall(30, 12, _currentSurface);
            new InnerWall(30, 13, _currentSurface);
            new InnerWall(30, 14, _currentSurface);
            new InnerWall(30, 15, _currentSurface);
            new InnerWall(30, 16, _currentSurface);
        }

        private void BuildTeleports()
        {
            var teleport1 = new Teleport(2, 7, _currentSurface);
            var teleport2 = new Teleport(22, 3, _currentSurface);
            teleport1.Bind(teleport2);
            teleport2.Bind(teleport1);
        }

        private void BuildIce()
        {
            new Ice(23, 18, _currentSurface);
            new Ice(23, 19, _currentSurface);
            new Ice(23, 20, _currentSurface);
            new Ice(23, 21, _currentSurface);
            new Ice(23, 22, _currentSurface);
            new Ice(23, 23, _currentSurface);
            new Ice(23, 24, _currentSurface);
            new Ice(23, 25, _currentSurface);
            new Ice(23, 26, _currentSurface);
            
            new Ice(26, 23, _currentSurface);
            new Ice(26, 24, _currentSurface);
            
            new Ice(29, 18, _currentSurface);
            new Ice(29, 19, _currentSurface);
            new Ice(29, 20, _currentSurface);
   
            new Ice(29, 25, _currentSurface);
            new Ice(29, 26, _currentSurface);
            
            new Ice(31, 18, _currentSurface);
            new Ice(31, 19, _currentSurface);
            new Ice(31, 20, _currentSurface);
            new Ice(31, 21, _currentSurface);
            new Ice(31, 22, _currentSurface);
            new Ice(31, 23, _currentSurface);
            new Ice(31, 24, _currentSurface);
            new Ice(31, 25, _currentSurface);
            new Ice(31, 26, _currentSurface);
            new Ice(32, 18, _currentSurface);
            new Ice(32, 19, _currentSurface);
            new Ice(32, 20, _currentSurface);
            new Ice(32, 21, _currentSurface);
            new Ice(32, 22, _currentSurface);
            new Ice(32, 23, _currentSurface);
            new Ice(32, 24, _currentSurface);
            new Ice(32, 25, _currentSurface);
            new Ice(32, 26, _currentSurface);
            new Ice(33, 18, _currentSurface);
            new Ice(33, 19, _currentSurface);
            new Ice(33, 20, _currentSurface);
            new Ice(33, 21, _currentSurface);
            new Ice(33, 22, _currentSurface);
            new Ice(33, 23, _currentSurface);
            new Ice(33, 24, _currentSurface);
            new Ice(33, 25, _currentSurface);
            new Ice(33, 26, _currentSurface);
            new Ice(34, 18, _currentSurface);
            new Ice(34, 19, _currentSurface);
            new Ice(34, 20, _currentSurface);
            new Ice(34, 21, _currentSurface);
            new Ice(34, 22, _currentSurface);
            new Ice(34, 23, _currentSurface);
            new Ice(34, 24, _currentSurface);
            new Ice(34, 25, _currentSurface);
            new Ice(34, 26, _currentSurface);
            new Ice(35, 18, _currentSurface);
            new Ice(35, 19, _currentSurface);
            new Ice(35, 20, _currentSurface);
            new Ice(35, 21, _currentSurface);
            new Ice(35, 22, _currentSurface);
            new Ice(35, 23, _currentSurface);
            new Ice(35, 24, _currentSurface);
            new Ice(35, 25, _currentSurface);
            new Ice(35, 26, _currentSurface);
            new Ice(36, 18, _currentSurface);
            new Ice(36, 19, _currentSurface);
            new Ice(36, 20, _currentSurface);
            new Ice(36, 21, _currentSurface);
            new Ice(36, 22, _currentSurface);
            new Ice(36, 23, _currentSurface);
            new Ice(36, 24, _currentSurface);
            new Ice(36, 25, _currentSurface);
            new Ice(36, 26, _currentSurface);
            new Ice(37, 18, _currentSurface);
            new Ice(37, 19, _currentSurface);
            new Ice(37, 20, _currentSurface);
            new Ice(37, 21, _currentSurface);
            new Ice(37, 22, _currentSurface);
            new Ice(37, 23, _currentSurface);
            new Ice(37, 24, _currentSurface);
            new Ice(37, 25, _currentSurface);
            new Ice(37, 26, _currentSurface);
            new Ice(38, 18, _currentSurface);
            new Ice(38, 19, _currentSurface);
            new Ice(38, 20, _currentSurface);
            new Ice(38, 21, _currentSurface);
            new Ice(38, 22, _currentSurface);
            new Ice(38, 23, _currentSurface);
            new Ice(38, 24, _currentSurface);
            new Ice(38, 25, _currentSurface);
            new Ice(38, 26, _currentSurface);
            new Ice(39, 18, _currentSurface);
            new Ice(39, 19, _currentSurface);
            new Ice(39, 20, _currentSurface);
            new Ice(39, 21, _currentSurface);
            new Ice(39, 22, _currentSurface);
            new Ice(39, 23, _currentSurface);
            new Ice(39, 24, _currentSurface);
            new Ice(39, 25, _currentSurface);
            new Ice(39, 26, _currentSurface);
            new Ice(40, 18, _currentSurface);
            new Ice(40, 19, _currentSurface);
            new Ice(40, 20, _currentSurface);
            new Ice(40, 21, _currentSurface);
            new Ice(40, 22, _currentSurface);
            new Ice(40, 23, _currentSurface);
            new Ice(40, 24, _currentSurface);
            new Ice(40, 25, _currentSurface);
            new Ice(40, 26, _currentSurface);
        }

        private void BuildLogicElements()
        {
            var key = new InventoryObject
            (
                "Key from door",
                InventoryObjectType.Key,
                "K",
                "A key that opens some door;\n" +
                "Try it on some door\n"
            );
            var keyCell = new InventoryCell(17, 3, _currentSurface, key);

            var stone = new InventoryObject
            (
                "stone",
                InventoryObjectType.Trash,
                "G",
                "This stone is just a stone;\n" +
                "It is doing nothing like all stones;\n" +
                "Why do you storage a garbage?\n"
            );
            var stoneCell = new InventoryCell(17, 15, _currentSurface, stone);

            var snowstepers = new InventoryObject
            (
                "Snowstepers",
                InventoryObjectType.Cloth,
                "A",
                "This equipment give you an opportunity safety cross an ice;\n" +
                "Don't forget to put it on before usage\n"
            );
            var snowstepersCell = new InventoryCell(36, 9, _currentSurface, snowstepers);
            
            var crystal = new InventoryObject
            (
                "Unstable crystal",
                InventoryObjectType.Cloth,
                "N",
                "Gnomes and elves love this crystal for its beauty;\n" +
                "In itself it is an ordinary stone that does nothing\n" +
                "\nMAY DESTABILIZE THE PORTAL!!!\n"
            );
            var crystalCell = new InventoryCell(7, 7, _currentSurface, crystal);

            var door1 = new Door(30, 3, _currentSurface, key);
            var door2 = new Door(5, 5, _currentSurface);
            var door3 = new Door(7, 10, _currentSurface, 18);
            var door4 = new Door(5, 17, _currentSurface, 5);
            var door5 = new Door(14, 17, _currentSurface, 0);
            var door6 = new Door(10, 22, _currentSurface, 3);
            var door7 = new Door(20, 22, _currentSurface, 0);
            var winDoor = new Door(38, 13, _currentSurface, 3);
            winDoor.IsWin = true;

            var button = new Button(28, 2, _currentSurface);
            button.Bind(door2);

            var message1 = "Use [^, <, v, >] or [W, A, S, D] to move\n" +
                           "Use [I] or [TAB] to open inventory(there you also can mange your items)\n" +
                           "Your aim is to find exit-door\n" +
                           "Cells:\n" +
                           "@ is you\n" +
                           "# is wall\n" +
                           ". is ground\n" +
                           "D is door(some doors can be closed, and open by different ways)\n" +
                           "c is coin\n" +
                           "Items can be marked with different symbols like 'G'";
            var helper1 = new Help(20, 13, _currentSurface, message1);

            // редактор жалуется на создание не используемых переменных,
            // но если создовать клсетки без переменный( только слово new ),
            // то он жалуется на то, что я не назначил объект, созданный выражением «new»
        }
    }
}