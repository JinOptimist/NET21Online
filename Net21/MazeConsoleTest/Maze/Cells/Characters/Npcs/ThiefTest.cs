using NUnit.Framework;
using Moq;
using MazeCore.Maze;
using MazeCore.Maze.Cells;
using MazeCore.Maze.Cells.Surface;
using System.Collections.Generic;
using MazeCore.Maze.Cells.Characters;
using MazeCore.Maze.Cells.Characters.Npcs;

namespace MazeConsoleTest.Maze.Cells.Characters.Npcs
{
    public class ThiefTest
    {
        private Mock<IMazeMap> _mazeMapMock;
        private Thief _thiefMock;
        private Mock<IHero> _heroMock;
        private Mock<IBaseCharacter> _baseCharacterMock;

        [SetUp]
        public void Setup()
        {
            _mazeMapMock = new Mock<IMazeMap>();
            _thiefMock = new Thief(2, 2, _mazeMapMock.Object);
            _heroMock = new Mock<IHero>();
            _baseCharacterMock = new Mock<IBaseCharacter>();

        }

        [Test]
        [TestCase(0)]
        [TestCase(100)]
        [TestCase(1)]

        public void TryStep_HeroLoseMoney(int initialCoins)
        {
            //Arrange
            _heroMock.SetupAllProperties();
            var hero = _heroMock.Object;
            hero.Money = initialCoins;

            //Act
            _thiefMock.TryStep(hero);

            //Result
            Assert.That(hero.Money == 0, "Coin is not 0 it's a problem");

        }

        [Test]
        public void TryStep_HeroCanStepOnTheThief()
        {
            //Act
            var result = _thiefMock.TryStep(_heroMock.Object);

            //Result
            Assert.That(result, Is.True, "Hero can't step the Thief");
        }

        [Test]
        public void TryStep_NPCCanNotStepTheThief()
        {
            //Act
            var result = _thiefMock.TryStep(_baseCharacterMock.Object);

            //Result
            Assert.That(result, Is.False, "NPC can step the Thief");
        }

        [Test]
        public void CellToMoveHeroReturned() //Не нашел способа без сохдания реального героя
        {
            //Arrange

            var hero = new Hero(2, 2, _mazeMapMock.Object);
            var nearCell = new List<BaseCell>
            {
                hero
            };
            _mazeMapMock.Setup(maze => maze.GetNearCell(_thiefMock))
                 .Returns(nearCell);

            //Act
            var result = _thiefMock.CellToMove();

            //Result
            Assert.That(result, Is.EqualTo(hero), "Hero in near cell is not returned");
        }

    }
}
