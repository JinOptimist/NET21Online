using MazeConsole.Maze;
using MazeConsole.Maze.Cells;
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

namespace MazeConsoleTest.Maze.Cells.Characters
{
    public class ThiefTest
    {
        private Mock<IMazeMap> _mazeMapMock;
        private Thief _thiefMock;
        private Hero _heroMock;
        private Mock<IBaseCharacter> _baseCharacterMock;

        [SetUp]
        public void Setup()
        {
            _mazeMapMock = new Mock<IMazeMap>();
            _thiefMock = new Thief(2, 2, _mazeMapMock.Object);
            _heroMock = new Hero(2, 3, _mazeMapMock.Object);
        }

        [Test]
        [TestCase(0)]
        [TestCase(100)]
        [TestCase(1)]

        public void TryStep_HeroLoseMoney(int initialCoins)
        {
            _heroMock.Money = initialCoins;
            _thiefMock.TryStep(_heroMock);
            Assert.That(_heroMock.Money == 0, "Coin is not 0 it's a problem");

        }

        [Test]
        public void TryStep_HeroCanStepOnTheThief()
        {
            var result = _thiefMock.TryStep(_heroMock);
            Assert.That(result, Is.True, "Hero can't step the Thief");
        }

        [Test]
        public void TryStep_NPCCanNotStepTheThief()
        {
            _baseCharacterMock = new Mock<IBaseCharacter>();
            var result = _thiefMock.TryStep(_baseCharacterMock.Object);
            Assert.That(result, Is.False, "NPC can step the Thief");
        }

        ////[Test]
        //public void CellToMoveHeroReturned()
        //{
        //    var nearCells = new List<IBaseCell>
        //    {
        //        _heroMock
        //    };

        //    var result = _thiefMock.CellToMove();
        //    Assert.That(result, Is.EqualTo(_heroMock), "Hero in near cell is not returned");
        //}

    }
}
