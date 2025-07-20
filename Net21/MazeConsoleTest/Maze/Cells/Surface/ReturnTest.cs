using MazeConsole.Maze;
using MazeConsole.Maze.Cells;
using MazeConsole.Maze.Cells.Сharacters;
using Moq;
using NUnit.Framework;

namespace MazeConsoleTest.Maze.Cells.Surface
{
    public class ReturnTest
    {
        private Mock<IMazeMap> _mazeMapMock;
        private Mock<IBaseCharacter> _baseCharacterMock;
        private Return _return;

        [SetUp]
        public void Setup()
        {
            _mazeMapMock = new Mock<IMazeMap>();
            _baseCharacterMock = new Mock<IBaseCharacter>();
            _return = new Return(5, 5, _mazeMapMock.Object);
        }

        [Test]
        public void TryStep_CheckChangeHeroCoordinates()
        {
            _baseCharacterMock.SetupAllProperties();
            var chapter = _baseCharacterMock.Object;
            _return.TryStep(chapter);
            Assert.That(chapter.X == 1 && chapter.Y == 1);
        }

        [Test]
        public void TryStep_ReturnFalse() //
        {
           var result = _return.TryStep(_baseCharacterMock.Object);
           Assert.That(result == false);
        }

        [Test]
        public void Symbol_CheckReturnCharacter()
        {
            Assert.That(_return.Symbol == "<");
        }
    }
}