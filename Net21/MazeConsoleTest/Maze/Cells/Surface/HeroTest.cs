using MazeConsole.Maze;
using MazeConsole.Maze.Cells;
using MazeConsole.Maze.Cells.Сharacters;
using MazeConsole.Maze.Cells.Inventory;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsoleTest.Maze.Cells.Surface
{
    public class HeroTest
    {
        private Mock<IMazeMap> _mazeMapMock;
        private Mock<IBaseCharacter> _baseCharacterMock;
        private Hero _hero;
        private Mock<IBaseItems> _inventory;



        [SetUp]
        public void Setup()
        {
            _mazeMapMock = new Mock<IMazeMap>();
            _baseCharacterMock = new Mock<IBaseCharacter>();
            _hero = new Hero(5, 5, _mazeMapMock.Object);
            _inventory = new Mock<IBaseItems>();
        }

        private IBaseItems CreateTestInventory()
        {
            _inventory.SetupAllProperties();
            var inventory = _inventory.Object;
            inventory.Name = "boat";
            return inventory;
        }

        [Test]
        [TestCase(2, 1)]
        [TestCase(5, 4)]
        [TestCase(1, 0)]
        public void TryStep_CheckHpNpcDecrease(int initialHp, int resultHp)
        {
            _baseCharacterMock.SetupAllProperties();
            var npc = _baseCharacterMock.Object;
            npc.Hp = initialHp;
            _hero.TryStep(npc);
            Assert.That(npc.Hp == resultHp);
        }
        public void TryStep_ReturnFalse()
        {
            var result = _hero.TryStep(_baseCharacterMock.Object);
            Assert.That(result == false);
        }

        [Test]
        public void GetInventoryNames_CheckReturnInventoryName()
        {
            var boat = CreateTestInventory();
            _hero.Inventory.Add((IBaseItems)boat);
            var inventory = _hero.GetInventoryNames();
            Assert.That(inventory.Count == 1 && inventory[0] == "boat");
        }
        [Test]
        [TestCase(2, true)]
        [TestCase(5, true)]
        [TestCase(10, false)]
        [TestCase(11, false)]
        public void CanGet_CheckReturnTrueOrFalse(int quantityInventory, bool result)
        {
            
            for (int i = 0; i < quantityInventory; i++)
            {
                var boat = CreateTestInventory();
                _hero.Inventory.Add((IBaseItems)boat);
            }
            Assert.That(_hero.CanGet() == result);
        }

        [Test]
        public void Symbol_CheckReturnCharacter()
        {
            Assert.That(_hero.Symbol == "@");
        }
    }
}
