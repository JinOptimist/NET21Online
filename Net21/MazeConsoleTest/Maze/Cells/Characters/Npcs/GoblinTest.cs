using MazeConsole.Maze;
using MazeConsole.Maze.Cells;
using MazeConsole.Maze.Cells.Сharacters;
using MazeConsole.Maze.Cells.Сharacters.Npcs;
using Moq;
using NUnit.Framework;

namespace MazeConsoleTest.Maze.Cells.Characters.Npcs
{
    [TestFixture]
    public class GoblinTests
    {
        private Goblin _goblin;
        private Hero _hero;
        private Mock<IMazeMap> _mazeMapMock;
        private Mock<IBaseCell> _groundMock;
        private Mock<IBaseCell> _wallMock;

        [SetUp]
        public void Setup()
        {
            _mazeMapMock = new Mock<IMazeMap>();
            _groundMock = new Mock<IBaseCell>();
            _wallMock = new Mock<IBaseCell>();

            // Configure ground mock
            _groundMock.Setup(g => g.TryStep(It.IsAny<IBaseCharacter>())).Returns(true);
            _groundMock.As<IBaseCell>().SetupGet(g => g.Symbol).Returns(".");

            // Configure wall mock
            _wallMock.Setup(w => w.TryStep(It.IsAny<IBaseCharacter>())).Returns(false);
            _wallMock.As<IBaseCell>().SetupGet(w => w.Symbol).Returns("#");
        }

        [Test]
        public void TryStep_WhenHeroStepsOnGoblin_BothLoseHp()
        {
            // Arrange
            _goblin = new Goblin(1, 1, _mazeMapMock.Object, hp: 2, money: 1);
            _hero = new Hero(1, 2, _mazeMapMock.Object) { Hp = 3 };

            // Act
            var result = _goblin.TryStep(_hero);

            // Assert
            Assert.That(result, Is.False);
            Assert.That(_hero.Hp, Is.EqualTo(2), "Hero should lose 1 HP");
            Assert.That(_goblin.Hp, Is.EqualTo(1), "Goblin should lose 1 HP");
        }

        [Test]
        public void TryStep_WhenHeroHas1Hp_ThenHeroDies()
        {
            // Arrange
            _goblin = new Goblin(1, 1, _mazeMapMock.Object, hp: 1, money: 1);
            _hero = new Hero(1, 2, _mazeMapMock.Object) { Hp = 1 };

            // Act
            var result = _goblin.TryStep(_hero);

            // Assert
            Assert.That(result, Is.False);
            Assert.That(_hero.Hp, Is.EqualTo(0), "Hero should die (HP = 0)");
            Assert.That(_goblin.Hp, Is.EqualTo(0), "Goblin should die (HP = 0)");
            Assert.That(_hero.Hp <= 0, Is.True, "hero died");
        }

        [Test]
        public void CellToMove_WhenHeroIsNear_ReturnsHeroCell()
        {
            // Arrange
            _goblin = new Goblin(1, 1, _mazeMapMock.Object, hp: 1, money: 1);
            _hero = new Hero(1, 2, _mazeMapMock.Object);

            _mazeMapMock.Setup(m => m.GetNearCell(_goblin))
                .Returns(new List<BaseCell> { _hero });

            // Act
            var result = _goblin.CellToMove();

            // Assert
            Assert.That(result, Is.SameAs(_hero));
        }

        [Test]
        public void CellToMove_WhenNoHeroNearby_ReturnsFirstGroundCell()
        {
            _goblin = new Goblin(1, 1, _mazeMapMock.Object, hp: 1, money: 1);

            var groundCell = _groundMock.Object as BaseCell;
            var wallCell = _wallMock.Object as BaseCell;

            _mazeMapMock.Setup(m => m.GetNearCell(_goblin))
                .Returns(new List<BaseCell> { wallCell, groundCell });

            // Act
            var result = _goblin.CellToMove();

            // Assert
            Assert.That(result, Is.SameAs(groundCell));
        }

        [Test]
        public void Symbol_AlwaysReturns_G()
        {
            // Arrange
            _goblin = new Goblin(1, 1, _mazeMapMock.Object, hp: 1, money: 1);

            // Act & Assert
            Assert.That(_goblin.Symbol, Is.EqualTo("g"));
        }
    }


}
