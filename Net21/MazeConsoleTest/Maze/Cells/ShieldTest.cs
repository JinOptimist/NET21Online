using MazeConsole.Maze.Cells.Surface;
using MazeConsole.Maze.Cells.Сharacters;
using MazeConsole.Maze;
using Moq;
using NUnit.Framework;
using MazeConsole.Maze.Cells;

namespace MazeConsoleTest.Maze.Cells
{
    public class ShieldTest
    {
        private Mock<IMazeMap> _mazeMapMock;
        private Mock<IBaseCharacter> _baseCharacterMock;
        private Shield _shield;

        [SetUp]
        public void Setup()
        {
            _mazeMapMock = new Mock<IMazeMap>();// Stub
            _baseCharacterMock = new Mock<IBaseCharacter>();// Stub
            _shield = new Shield(28, 23, _mazeMapMock.Object); // Real
        }

        [Test]
        [TestCase(1, 3)]
        [TestCase(10, 12)]
        public void TryStep_CheckThatHpIsIncrease(int initialHp, int resultHp)
        {
            // Arrange / preparation
            _baseCharacterMock.SetupAllProperties();
            var hero = _baseCharacterMock.Object;
            hero.Hp = initialHp;

            //  Act
            _shield.TryStep(hero);

            // Assert
            Assert.That(hero.Hp == resultHp, "Shield add 2 hp it's a problem");
        }

        [Test]
        public void TryStep_ReturnTrue()
        {
            // Arrange / preparation

            //  Act
            var result = _shield.TryStep(_baseCharacterMock.Object);

            // Assert
            Assert.That(result == true, "I must have possibility step on shield");
        }

        [Test]
        public void TryStep_CellWasReplaced()
        {
            // Arrange / preparation

            // Act
            _shield.TryStep(_baseCharacterMock.Object);

            // Assert
            _mazeMapMock.Verify(maze => maze.ReplaceCell(It.IsAny<Ground>()), Times.Once);
        }
    }
}