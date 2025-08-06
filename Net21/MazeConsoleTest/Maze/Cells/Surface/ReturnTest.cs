using MazeCore.Maze;
using MazeCore.Maze.Cells.Characters;
using MazeCore.Maze.Cells.Surface;
using Moq;
using NUnit.Framework;

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

            _return.TryStep(hero);

            Assert.That(hero.X == 1 && hero.Y == 1);
            Assert.That(hero.X == 1 && hero.Y == 1);
        }
    }
}