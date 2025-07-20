using MazeConsole.Maze;
using MazeConsole.Maze.Cells.Surface;
using MazeConsole.Maze.Cells.Characters;
using NUnit.Framework;
using Moq;

namespace MazeConsoleTest.Maze.Cells.Surfase
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
            var hero = _baseCharacterMock.Object;
            hero.X = 4;
            hero.Y = 4;

            _returnTryStep(hero);

            Assert.That(hero.X == 1 && hero.Y == 1, )
            Assert.That(hero.X == 1 && hero.Y == 1, )
        }
    }
}