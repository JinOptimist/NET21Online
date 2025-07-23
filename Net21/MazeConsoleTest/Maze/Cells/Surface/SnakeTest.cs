using MazeConsole.Maze;
using MazeConsole.Maze.Cells;
using MazeConsole.Maze.Cells.Characters;
using Moq;
using NUnit.Framework;

namespace MazeConsoleTest.Maze.Cells.Surface
{
    public class SnakeTest
    {
        [Test]
        [TestCase(4, 3)]
        [TestCase(50, 49)]
        [TestCase(100, 99)]

        public void TryStep_SnakeTakesHp(int initalSnake, int resultSnake)
        {
            //Arrange / preparation
            var mazeMapMock = new Mock<IMazeMap>();
            var mazeMap = mazeMapMock.Object;
            var snake = new Snake(1, 1, mazeMap);
            var heroMock = new Mock<IHero>();
            heroMock.SetupAllProperties();
            var hero = heroMock.Object;
            hero.Hp = initalSnake;

            //Act
            snake.TryStep(hero);

            //Assert
            Assert.That(hero.Hp == resultSnake, "Hp is not -1 it's a problem");            
        }
    }
    
}
