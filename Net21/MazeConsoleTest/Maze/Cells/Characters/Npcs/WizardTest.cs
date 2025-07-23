using NUnit.Framework;
using Moq;
using MazeCore.Maze;
using MazeCore.Maze.Cells;
using MazeCore.Maze.Cells.Surface;
using System.Collections.Generic;
using MazeCore.Maze.Cells.Characters;
using MazeCore.Maze.Cells.Characters.Npcs;

namespace MazeCore.Tests.Maze.Cells.Сharacters.Npcs
{
    [TestFixture]
    public class WizardTests
    {
        private Wizard _wizard;
        private Mock<IMazeMap> _mazeMapMock;
        private Mock<IBaseCharacter> _characterMock;
        private Mock<IBaseCell> _wallMock;
        private Mock<IBaseCell> _groundMock;
        private Mock<IBaseCell> _heroMock;

        [SetUp]
        public void Setup()
        {
            _mazeMapMock = new Mock<IMazeMap>();
            _characterMock = new Mock<IBaseCharacter>();
            _characterMock.SetupProperty(c => c.Hp);
            _wallMock = new Mock<IBaseCell>();
            _groundMock = new Mock<IBaseCell>(); 
            _heroMock =  new Mock<IBaseCell>();
        }
        private Mock<IBaseCell> ConfigureCellMock(Mock<IBaseCell> mock, int x, int y)
        {
            mock.SetupGet(c => c.X).Returns(x);
            mock.SetupGet(c => c.Y).Returns(y);
            mock.SetupGet(c => c.MazeMap).Returns(_mazeMapMock.Object);
            return mock;
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
        
            // Настраиваем моки
            ConfigureCellMock(_heroMock, initialX + 1, initialY);
            ConfigureCellMock(_wallMock, initialX, initialY + 1);
            ConfigureCellMock(_groundMock, initialX - 1, initialY);

            _mazeMapMock.Setup(m => m.GetNearCell(_wizard))
                .Returns(new List<BaseCell> { 
                    _heroMock.Object as BaseCell, 
                    _wallMock.Object as BaseCell, 
                    _groundMock.Object as BaseCell 
                });
        
            var result = _wizard.CellToMove();
            Assert.That(result, Is.SameAs(_heroMock.Object as BaseCell));
        }

        [Test]
        [TestCase(10, 2, 3)]
        public void CellToMove_ReturnsFirstNonWallCell_WhenNoHeroNearby(int initialHp, int initialX, int initialY)
        {
            _wizard = new Wizard(initialX, initialY, _mazeMapMock.Object, true, initialHp);
        
            ConfigureCellMock(_wallMock, initialX, initialY + 1);
            ConfigureCellMock(_groundMock, initialX - 1, initialY);

            _mazeMapMock.Setup(m => m.GetNearCell(_wizard))
                .Returns(new List<BaseCell> { 
                    _wallMock.Object as BaseCell, 
                    _groundMock.Object as BaseCell 
                });
        
            var result = _wizard.CellToMove();
            Assert.That(result, Is.SameAs(_groundMock.Object as BaseCell));
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