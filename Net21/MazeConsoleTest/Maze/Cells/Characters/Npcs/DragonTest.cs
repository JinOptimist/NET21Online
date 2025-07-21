using MazeConsole.Maze;
using MazeConsole.Maze.Cells;
using MazeConsole.Maze.Cells.Characters.Npcs;
using MazeConsole.Maze.Cells.Surface;
using MazeConsole.Maze.Cells.Сharacters;
using MazeConsole.Maze.Cells.Сharacters.Npcs;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsoleTest.Maze.Cells.Characters.Npcs
{
    public class DragonTest
    {
        private Mock<IMazeMap> _mazeMapMock;
        private Mock<IBaseCharacter> _baseCharacterMock;
        private Dragon _dragon;

        [SetUp]
        public void Setup()
        {
            _mazeMapMock = new Mock<IMazeMap>();
            _baseCharacterMock = new Mock<IBaseCharacter>();
            _dragon = new Dragon(1, 1, _mazeMapMock.Object, 10, 10);
        }

        [Test]
        public void TryStep_DragonRetutnTrue()
        {
            var resault = _dragon.TryStep(_dragon);

            Assert.That(resault, "Dragon can't step on the Dragon. It's bad!");
        }

        [Test]
        public void TryStep_NotDragonRetutnFalse()
        {
            var resault = _dragon.TryStep(_baseCharacterMock.Object);

            Assert.That(!resault, "notDragon can step on the Dragon. Is a bad!");
        }

        [Test]
        [TestCase(1, -2)] 
        [TestCase(10, 7)]
        [TestCase(1000, 997)]
        public void TryStep_CheckThatHeroHpIsIncrease(int hpStart, int hpResult)
        {
            var heroMock = new Mock<IHero>();
            heroMock.SetupAllProperties();

            var hero = heroMock.Object;

            hero.Hp = hpStart;

            _dragon.TryStep(hero);

            Assert.That(hero.Hp == hpResult, $"Hp is not {hpResult}. It's a problem!");
        }

        [Test]
        [TestCase(1, 0)]
        [TestCase(2, 1)]
        [TestCase(12, 11)]
        [TestCase(1000, 999)]
        public void TryStep_CheckThatDragonHpIsIncrease(int hpStart, int hpResult)
        {
            _mazeMapMock.Setup(x => x.Npcs).Returns(new List<BaseNpc> { _dragon });

            var hero = new Mock<IHero>().SetupAllProperties().Object;

            _dragon.Hp = hpStart;

            _dragon.TryStep(hero);

            Assert.That(_dragon.Hp == hpResult, $"Hp Dragon's not {hpResult}. It's a problem!");
        }

        [Test]
        public void TryStep_RemoveDragonWhenDragonHas0Hp()
        {
            _mazeMapMock.Setup(x => x.Npcs).Returns(new List<BaseNpc> { _dragon });
            _dragon.Hp = 0;

            _dragon.TryStep(_baseCharacterMock.Object);
            Assert.That(_mazeMapMock.Object.Npcs.Count == 0, "Dragon wasn't delete!"); 
        }

        [Test]
        public void TryStep_CellWasReplacedOnCoin()
        {
            _mazeMapMock.Setup(x => x.Npcs).Returns(new List<BaseNpc> { _dragon });
            _dragon.Hp = 0;
            _dragon.TryStep(_baseCharacterMock.Object);

            _mazeMapMock.Verify(maze => maze.ReplaceCell(It.IsAny<Coin>()), Times.Once);
        }



        [Test]
        public void CellToMove_ReturnHero()
        {
            var hero = new Hero(2, 2, _mazeMapMock.Object);

            var nearCells = new List<BaseCell> { hero, new Ground(1, 2, _mazeMapMock.Object), new Ground(2, 1, _mazeMapMock.Object) };

            _mazeMapMock.Setup(m => m.GetCellsInRadius(_dragon, 3)).Returns(nearCells);

            Assert.That(_dragon.CellToMove(), Is.SameAs(hero));
        }

        [Test]
        public void CellToMove_ReturnFirstGround()
        {
            var ground1 = new Ground(1, 2, _mazeMapMock.Object);
            var ground2 = new Ground(2, 1, _mazeMapMock.Object);

            var nearCells = new List<BaseCell> { ground1, ground2 };

            _mazeMapMock.Setup(m => m.GetCellsInRadius(_dragon, 3)).Returns(nearCells);

            Assert.That(_dragon.CellToMove(), Is.SameAs(ground1));
        }

        [Test]
        public void CellToMove_ReturnNull()
        {
            _mazeMapMock.Setup(m => m.GetCellsInRadius(_dragon, 3)).Returns(new List<BaseCell>());
            Assert.That(_dragon.CellToMove(), Is.Null);
        }
    }
}
