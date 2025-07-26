using NUnit.Framework;
using Moq;
using MazeCore.Maze;
using MazeCore.Maze.Cells;
using MazeCore.Maze.Cells.Surface;
using System.Collections.Generic;
using MazeCore.Maze.Cells.Characters;
using MazeCore.Maze.Cells.Characters.Npcs;

namespace MazeConsoleTest.Maze.Cells.Surface
{
    public class IceTest
    {
        private Mock<IMazeMap> _mazeMapMock;
        private Mock<IBaseCharacter> _baseCharacterMock;
        private Mock<Hero> _heroMock;
        private Ice _iceMock;
        private Mock<Ground> _groundMock;
        private Mock<Wall> _wallMock;

        [SetUp]
        public void Setup()
        {
            _mazeMapMock = new Mock<IMazeMap>();
            _baseCharacterMock = new Mock<IBaseCharacter>();
            _iceMock = new Ice(3, 4, _mazeMapMock.Object);
            _groundMock = new Mock<Ground>(4, 5, _mazeMapMock.Object);
            _wallMock = new Mock<Wall>(4, 5, _mazeMapMock.Object);
            var hero = _baseCharacterMock;

            _baseCharacterMock.SetupProperty(cell => cell.X, 2);
            _baseCharacterMock.SetupProperty(cell => cell.Y, 3);
            _mazeMapMock.Setup(cell => cell[3, 4]).Returns(_iceMock);
        }

        [Test]
        public void TryStep_CheckThatHeroChangeCoordinate()
        {
            // Arrange

            _mazeMapMock.Setup(cell => cell[4, 5]).Returns(_groundMock.Object);
            _groundMock.Setup(ground => ground.TryStep(It.IsAny<IBaseCharacter>())).Returns(true);

            //Act
            _iceMock.TryStep(_baseCharacterMock.Object);

            // Assert
            Assert.That(_baseCharacterMock.Object.X, Is.EqualTo(4));
            Assert.That(_baseCharacterMock.Object.Y, Is.EqualTo(5));
        }

        [Test]
        public void TryStep_HeroCanSlideMoreThanOneIce()
        {
            //Arrange
            _mazeMapMock.Setup(cell => cell[4, 5]).Returns(_iceMock);
            _mazeMapMock.Setup(cell => cell[5, 6]).Returns(_groundMock.Object);
            _groundMock.Setup(ground => ground.TryStep(It.IsAny<IBaseCharacter>())).Returns(true);

            //Act 
            _iceMock.TryStep(_baseCharacterMock.Object);


            // Assert
            Assert.That(_baseCharacterMock.Object.X, Is.EqualTo(5));
            Assert.That(_baseCharacterMock.Object.Y, Is.EqualTo(6));
        }

        [Test]
        [TestCase(9, 8)]
        [TestCase(0, -1)]

        public void TryStep_HeroGetDamage(int initialHp, int resultHp)
        {
            //Arrange
            _baseCharacterMock.SetupAllProperties();
            _baseCharacterMock.SetupProperty(cell => cell.X, 2);
            _baseCharacterMock.SetupProperty(cell => cell.Y, 3);
            _baseCharacterMock.Object.Hp = initialHp;
            _mazeMapMock.Setup(wall => wall[4, 5]).Returns(_wallMock.Object);
            _wallMock.Setup(wall => wall.TryStep(It.IsAny<IBaseCharacter>())).Returns(false);

            //Act
            _iceMock.TryStep(_baseCharacterMock.Object);

            //Result
            Assert.That(_baseCharacterMock.Object.Hp == resultHp, "Coin is not 1 it's a problem");
        }

        [Test]
        [TestCase(9, 9)]
        [TestCase(0, 0)]

        public void TryStep_HeroDoNotGetDamageWithoutWall(int initialHp, int resultHp)
        {
            //Arrange
            _baseCharacterMock.SetupAllProperties();
            _baseCharacterMock.SetupProperty(cell => cell.X, 2);
            _baseCharacterMock.SetupProperty(cell => cell.Y, 3);
            _baseCharacterMock.Object.Hp = initialHp;
            _mazeMapMock.Setup(ground => ground[4, 5]).Returns(_groundMock.Object);
            _groundMock.Setup(ground => ground.TryStep(_baseCharacterMock.Object)).Returns(true);

            //Act
            _iceMock.TryStep(_baseCharacterMock.Object);

            //Result
            Assert.That(_baseCharacterMock.Object.Hp, Is.EqualTo(initialHp));
        }

        [Test]
        public void CheckIceSymbol()
        {
            //Result
            Assert.That(_iceMock.Symbol, Is.EqualTo("%"));
        }
    }
}
