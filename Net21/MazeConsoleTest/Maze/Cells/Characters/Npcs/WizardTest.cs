using NUnit.Framework;
using Moq;
using MazeConsole.Maze;
using MazeConsole.Maze.Cells;
using MazeConsole.Maze.Cells.Surface;
using MazeConsole.Maze.Cells.Сharacters;
using MazeConsole.Maze.Cells.Сharacters.Npcs;
using System.Collections.Generic;

namespace MazeConsole.Tests.Maze.Cells.Сharacters.Npcs
{
    [TestFixture]
    public class WizardTests
    {
        private Wizard _wizard;
        private Mock<IMazeMap> _mazeMapMock;
        private Mock<IBaseCharacter> _characterMock;

        [SetUp]
        public void Setup()
        {
            _mazeMapMock = new Mock<IMazeMap>();
            _characterMock = new Mock<IBaseCharacter>();
            _characterMock.SetupProperty(c => c.Hp);
        }

        [Test]
        [TestCase(10, 2, 3)]
        public void Constructor_SetsPropertiesCorrectly(int initialHp, int initialX, int initialY)
        {
            _wizard = new Wizard(initialX, initialY, _mazeMapMock.Object, true, initialHp);
            Assert.That(_wizard.X, Is.EqualTo(initialX));
            Assert.That(_wizard.Y, Is.EqualTo(initialY));
            Assert.That(_wizard.MazeMap, Is.SameAs(_mazeMapMock.Object));
            Assert.That(_wizard.Hp, Is.EqualTo(initialHp));
            Assert.That(_wizard.IsGoodMood, Is.True);
            Assert.That(_wizard.Symbol, Is.EqualTo("?"));
        }

        [Test]
        [TestCase(10, 2, 3)]
        public void TryStep_GoodMood_DoublesCharacterHp(int initialHp, int initialX, int initialY)
        {
            _wizard = new Wizard(initialX, initialY, _mazeMapMock.Object, true, initialHp);
            _characterMock.Object.Hp = 5;
            var result = _wizard.TryStep(_characterMock.Object);
            Assert.That(_characterMock.Object.Hp, Is.EqualTo(10));
            Assert.That(_wizard.Hp, Is.EqualTo(0));
            Assert.That(result, Is.True);
        }

        [Test]
        [TestCase(10, 2, 3)]
        public void TryStep_BadMood_SetsCharacterHpToOne(int initialHp, int initialX, int initialY)
        {
            _wizard = new Wizard(initialX, initialY, _mazeMapMock.Object, false, initialHp);
            _characterMock.Object.Hp = 5;
            var result = _wizard.TryStep(_characterMock.Object);
            Assert.That(_characterMock.Object.Hp, Is.EqualTo(1));
            Assert.That(_wizard.Hp, Is.EqualTo(0));
            Assert.That(result, Is.True);
        }

        [Test]
        [TestCase(10, 2, 3)]
        public void CellToMove_ReturnsHero_WhenNearby(int initialHp, int initialX, int initialY)
        {
            _wizard = new Wizard(initialX, initialY, _mazeMapMock.Object, true, initialHp);
            var hero = new Hero(initialX + 1, initialY, _mazeMapMock.Object);
            var wall = new Wall(initialX, initialY + 1, _mazeMapMock.Object);
            var ground = new Ground(initialX - 1, initialY, _mazeMapMock.Object);

            _mazeMapMock.Setup(m => m.GetNearCell(_wizard))
                .Returns(new List<BaseCell> { hero, wall, ground });
            var result = _wizard.CellToMove();
            Assert.That(result, Is.SameAs(hero));
        }

        [Test]
        [TestCase(10, 2, 3)]
        public void CellToMove_ReturnsFirstNonWallCell_WhenNoHeroNearby(int initialHp, int initialX, int initialY)
        {
            _wizard = new Wizard(initialX, initialY, _mazeMapMock.Object, true, initialHp);
            var wall = new Wall(initialX, initialY + 1, _mazeMapMock.Object);
            var ground = new Ground(initialX - 1, initialY, _mazeMapMock.Object);

            _mazeMapMock.Setup(m => m.GetNearCell(_wizard))
                .Returns(new List<BaseCell> { wall, ground });
            var result = _wizard.CellToMove();
            Assert.That(result, Is.SameAs(ground));
        }

        [Test]
        [TestCase(10, 2, 3)]
        public void Symbol_AlwaysReturnsQuestionMark(int initialHp, int initialX, int initialY)
        {
            _wizard = new Wizard(initialX, initialY, _mazeMapMock.Object, true, initialHp);
            Assert.That(_wizard.Symbol, Is.EqualTo("?"));
        }
    }
}