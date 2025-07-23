using MazeCore.Maze;
using MazeCore.Maze.Cells.Characters;
using MazeCore.Maze.Cells.Surface;
using Moq;
using NUnit.Framework;

namespace MazeCoreTest.Maze.Cells.Surface
{
    public class CoinTest
    {
        private Mock<IMazeMap> _mazeMapMock;
        private Mock<IBaseCharacter> _baseCharacterMock;
        private Coin _coin;

        [SetUp]
        public void Setup()
        {
            _mazeMapMock = new Mock<IMazeMap>();// Stub
            _baseCharacterMock = new Mock<IBaseCharacter>();// Stub
            _coin = new Coin(42, 74, _mazeMapMock.Object); // Real
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(4, 5)]
        public void TryStep_CheckThatMoneyIsIncrease(int initialCoins, int resultCoins)
        {
            // Arrange / preparation
            _baseCharacterMock.SetupAllProperties();
            var hero = _baseCharacterMock.Object;
            hero.Money = initialCoins;

            //  Act
            _coin.TryStep(hero);

            // Assert
            Assert.That(hero.Money == resultCoins, "Coin is not 1 it's a problem");
        }

        [Test]
        public void TryStep_ReturnTrue()
        {
            // Arrange / preparation

            //  Act
            var result = _coin.TryStep(_baseCharacterMock.Object);

            // Assert
            Assert.That(result == true, "I must have possibility step to coin");
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-42)]
        public void TryStep_ThrowExceptionWithNegativaInitialCoin(int initialCoins)
        {
            // Arrange / preparation
            var mazeMapMock = new Mock<IMazeMap>();
            var mazeMap = mazeMapMock.Object; // Stub

            // Act
            // Assert
            Assert.Throws<ArgumentException>(
                () => new Coin(42, 74, mazeMap, initialCoins),
                "We expect exception with negative coins");
        }

        [Test]
        public void TryStep_CellWasReplaced()
        {
            // Arrange / preparation

            // Act
            _coin.TryStep(_baseCharacterMock.Object);

            // Assert
            _mazeMapMock.Verify(maze => maze.ReplaceCell(It.IsAny<Ground>()), Times.Once);
        }
    }
}
