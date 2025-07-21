using MazeConsole.Maze.Cells.Сharacters;
using MazeConsole.Maze;
using Moq;
using NUnit.Framework;
using MazeConsole.Maze.Cells.Surface;
using NUnit.Framework.Legacy;

namespace MazeConsoleTest.Maze.Cells.Surface
{
    public class TrapTest
    {
        private Mock<IMazeMap> _mazeMapMock;
        private Trap _trapMock;
        private Hero _heroMock;
        private Mock<IBaseCharacter> _baseCharacterMock;

        [SetUp]
        public void Setup()
        {
            _mazeMapMock = new Mock<IMazeMap>();
        }

        [Test]
        [TestCase(2, 0, "The hero died in a trap")]
        [TestCase(3, 1, null)]
        public void TryStep_HeroStepsOnTrap_HandlesDeathOrSurvival(int startHp, int expectedHp, string? expectedMessage)
        {
            // Arrange
            var realMazeMap = new MazeMap(5, 5);
            _trapMock = new Trap(2, 2, realMazeMap);
            _baseCharacterMock.Object.Hp = startHp;

            using var output = new StringWriter();
            Console.SetOut(output);

            // Act
            var result = _trapMock.TryStep(_baseCharacterMock.Object);

            // Assert
            Assert.That(_baseCharacterMock.Object.Hp, Is.EqualTo(expectedHp));
            Assert.That(result, Is.True);

            var consoleOutput = output.ToString();
            if (expectedMessage != null)
            {
                StringAssert.Contains(expectedMessage, consoleOutput);
            }
            else
            {
                Assert.That(consoleOutput, Is.Empty);
            }
        }
    }
}