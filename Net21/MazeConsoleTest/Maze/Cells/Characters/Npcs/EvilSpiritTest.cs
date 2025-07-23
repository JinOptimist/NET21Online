using MazeConsole.Maze.Cells.Surface;
using MazeConsole.Maze.Cells.Сharacters;
using MazeConsole.Maze.Cells.Сharacters.Npcs;
using MazeConsole.Maze;
using Moq;
using NUnit.Framework;
using MazeConsole.Maze.Cells;

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
            _baseCharacterMock = new Mock<IBaseCharacter>();// Stub
            _evilSpirit = new EvilSpirit(0, 0, _mazeMapMock.Object);//real
        }

        [Test]
        public void CellToMove_WhenHeroIsNear_ReturnsHero()
        {
            // Arrange
            var heroMock = new Mock<Hero>(1, 0, _mazeMapMock.Object);
            var groundMock = new Mock<BaseCell>(1, 1, _mazeMapMock.Object);

            _mazeMapMock.Setup(m => m.GetNearCell(It.IsAny<BaseCell>()))
                .Returns(new BaseCell[] { heroMock.Object, groundMock.Object });

            // Act & Assert
            Assert.That(_evilSpirit.CellToMove(), Is.SameAs(heroMock.Object));
        }

        [Test]
        public void CellToSecondMoveGround()
        {
            // Arrange
            var groundMock = new Mock<Ground>(1, 1, _mazeMapMock.Object);
            var wallMock = new Mock<Wall>(1, 0, _mazeMapMock.Object);

            _mazeMapMock.Setup(m => m.GetNearCell(It.IsAny<BaseCell>()))
                .Returns(new BaseCell[] { groundMock.Object, wallMock.Object });

            // Act
            var result = _evilSpirit.CellToMove();

            // Assert
            Assert.That(result, Is.SameAs(groundMock.Object));
        }

        [Test]
        public void CellToMoveNoGroundNull()
        {
            // Arrange
            var wallMock = new Mock<BaseCell>(1, 0, _mazeMapMock.Object);

            _mazeMapMock.Setup(m => m.GetNearCell(It.IsAny<BaseCell>()))
                .Returns(new BaseCell[] { wallMock.Object });

            // Act
            var result = _evilSpirit.CellToMove();

            // Assert
            Assert.That(result, Is.Null);
        }
        [Test]
        public void TryStepEvilSpirit()
        {
            // Arrange
            var otherEvilSpirit = new EvilSpirit(1, 0, _mazeMapMock.Object);

            // Act
            var result = _evilSpirit.TryStep(otherEvilSpirit);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void TryStepDecreasesHp()
        {
            // Arrange
            _baseCharacterMock.SetupAllProperties();
            var hero = _baseCharacterMock.Object;
            hero.Hp = 1;

            // Act
            var result = _evilSpirit.TryStep(hero);

            // Assert
            Assert.That(result, Is.False);
            Assert.That(hero.Hp, Is.EqualTo(0));
        }
    }
}