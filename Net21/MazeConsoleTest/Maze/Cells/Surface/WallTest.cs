using System;
using MazeConsole.Maze;
using MazeConsole.Maze.Cells.Surface;
using MazeConsole.Maze.Cells.Сharacters;
using Moq;
using NUnit.Framework;


namespace MazeConsoleTest.Maze.Cells.Surface
{
    public class WallTest
    {
        private Mock<IMazeMap> _mazeMapMock;
        private Mock<IBaseCharacter> _baseCharacterMock;
        private Wall _wall;

        [SetUp]
        public void Setup()
        {
            _mazeMapMock = new Mock<IMazeMap>();
            _baseCharacterMock = new Mock<IBaseCharacter>();
            _wall = new Wall(0, 0, _mazeMapMock.Object);
        }

        [Test]
        public void TryStep_AlwaysReturnFalse()
        {
            Assert.That(!_wall.TryStep(_baseCharacterMock.Object), "Someone can step to the wall. It's a problem.");
        }

    }
}
