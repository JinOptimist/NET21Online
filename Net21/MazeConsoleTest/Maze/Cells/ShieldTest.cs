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

        [Test]
        [TestCase(5, 7)]
        [TestCase(1, 3)]
        [TestCase(0, 2)]    //shield base logic doesn't cheack hero HP
        [TestCase(-1, 1)]   //shield base logic doesn't cheack hero HP
        public void TryStepHPincrease(int initialHp, int expectedHp)
        {
            // Arrange
            var mazeMapMock = new Mock<IMazeMap>();
            var shield = new Shield(0, 0, mazeMapMock.Object);

            var heroMock = new Mock<IBaseCharacter>();
            heroMock.SetupAllProperties();
            heroMock.Object.Hp = initialHp;

            // Act
            bool result = shield.TryStep(heroMock.Object);

            // Assert
            Assert.That(result, Is.True, "Shield ALWAYS returns true (current implementation)");
            Assert.That(heroMock.Object.Hp, Is.EqualTo(expectedHp),
                $"HP increases from {initialHp} to {expectedHp} (even if it's invalid)");

            // WARNING!
            if (initialHp <= 0)
            {
                Assert.Warn("!!!Shield heals even when HP is 0 or negative! This may be a bug!!!");
            }
        }
    }
}