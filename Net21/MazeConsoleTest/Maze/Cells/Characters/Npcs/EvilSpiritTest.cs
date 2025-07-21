using MazeConsole.Maze.Cells.Surface;
using MazeConsole.Maze.Cells.Сharacters;
using MazeConsole.Maze.Cells.Сharacters.Npcs;
using MazeConsole.Maze;
using Moq;
using NUnit.Framework;
using MazeConsole.Maze.Cells;
using MazeConsole.Maze.Cells.Characters.Npcs;

namespace MazeConsole.Tests.Maze.Cells.Сharacters.Npcs
{
    [TestFixture]
    public class EvilSpiritTests
    {
        private Mock<IMazeMap> _mazeMapMock;
        private Mock<IBaseCharacter> _baseCharacterMock;
        private EvilSpirit _evilSpirit;

        [SetUp]
        public void Setup()
        {
            _mazeMapMock = new Mock<IMazeMap>();//stub
            _baseCharacterMock = new Mock<IBaseCharacter>();//stub
            _evilSpirit = new EvilSpirit(0, 0, _mazeMapMock.Object);//real
        }

        [Test]
        public void CellToFirstMoveHero()
        {
            var hero = new Hero(1, 0, _mazeMapMock.Object);
            var ground = new Ground(0, 1, _mazeMapMock.Object);

            _mazeMapMock.Setup(m => m.GetNearCell(It.IsAny<BaseCell>()))
                .Returns(new BaseCell[] { hero, ground });

            // Act
            var result = _evilSpirit.CellToMove();

            // Assert
            Assert.That(result,Is.SameAs(hero));
        }

        [Test]
        public void CellToSecondMoveGround()
        {
            // Arrange
            var ground = new Ground(0, 1, _mazeMapMock.Object);
            var wall = new Wall(1, 0, _mazeMapMock.Object);

            _mazeMapMock.Setup(m => m.GetNearCell(It.IsAny<BaseCell>()))
                .Returns(new BaseCell[] { ground, wall });

            // Act
            var result = _evilSpirit.CellToMove();

            // Assert
            Assert.That(result,Is.SameAs(ground));
        }

        [Test]
        public void CellToMoveNoGroundNull()
        {
            // Arrange
            var wall = new Wall(1, 0, _mazeMapMock.Object);

            _mazeMapMock.Setup(m => m.GetNearCell(It.IsAny<BaseCell>()))
                .Returns(new BaseCell[] { wall });

            // Act
            var result = _evilSpirit.CellToMove();

            // Assert
            Assert.That(result,Is.Null);
        }
        [Test]
        public void TryStepEvilSpirit()
        {
            // Arrange
            var otherEvilSpirit = new EvilSpirit(1, 0, _mazeMapMock.Object);

            // Act
            var result = _evilSpirit.TryStep(otherEvilSpirit);

            // Assert
            Assert.That(result,Is.True);
        }

        [Test]
        public void TryStepDecreasesHp()
        {
            // Arrange
            var hero = new Hero(1, 0, _mazeMapMock.Object) { Hp = 1};
            var initialHp = hero.Hp;

            // Act
            var result = _evilSpirit.TryStep(hero);

            // Assert
            Assert.That(result,Is.False);
            Assert.That(hero.Hp, Is.EqualTo(initialHp - 1));
        }
    }
}