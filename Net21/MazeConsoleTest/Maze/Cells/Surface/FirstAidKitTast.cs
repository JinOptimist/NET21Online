using NUnit.Framework;
using Moq;
using MazeCore.Maze;
using MazeCore.Maze.Cells;
using MazeCore.Maze.Cells.Surface;
using MazeCore.Maze.Cells.Characters;

namespace MazeCore.Tests.Maze.Cells.Surface
{
    [TestFixture]
    public class FirstAidKitTests
    {
        private FirstAidKit _firstAidKit;
        private Mock<IMazeMap> _mazeMapMock;
        private Mock<IBaseCharacter> _characterMock;

        [SetUp]
        public void Setup()
        {
            _mazeMapMock = new Mock<IMazeMap>();
            _characterMock = new Mock<IBaseCharacter>();
            _firstAidKit = new FirstAidKit(10, 20, _mazeMapMock.Object);
            _characterMock.SetupProperty(c => c.Hp);
            _characterMock.Object.Hp = 5;
        }

        [Test]
        [TestCase(10,20)]
        public void Constructor_SetsPropertiesCorrectly(int initialX, int initialY)
        {
            Assert.That(_firstAidKit.X, Is.EqualTo(initialX));
            Assert.That(_firstAidKit.Y, Is.EqualTo(initialY));
            Assert.That(_firstAidKit.MazeMap, Is.SameAs(_mazeMapMock.Object));
            Assert.That(_firstAidKit.Symbol, Is.EqualTo("+"));
        }

        [Test]
        [TestCase(5,7)]
        public void TryStep_IncreasesCharacterHp(int initialHp,int expectedHp)
        {
            _characterMock.Object.Hp = initialHp;
            var result = _firstAidKit.TryStep(_characterMock.Object);
            Assert.That(_characterMock.Object.Hp, Is.EqualTo(expectedHp));
            Assert.That(result, Is.True);
        }

        [Test]
        [TestCase(10,20)]
        public void TryStep_ReplacesCellWithGround(int initialX, int initialY)
        {
            var expectedGround = new Ground(initialX, initialY, _mazeMapMock.Object);
            _firstAidKit.TryStep(_characterMock.Object);
            _mazeMapMock.Verify(m => m.ReplaceCell(
                It.Is<Ground>(g => g.X == initialX && g.Y == initialY && g.MazeMap == _mazeMapMock.Object)), 
                Times.Once);
        }

        [Test]
        public void TryStep_ReturnsTrue()
        {
            var result = _firstAidKit.TryStep(_characterMock.Object);
            Assert.That(result, Is.True);
        }

        [Test]
        public void Symbol_ReturnsCorrectValue()
        {
            Assert.That(_firstAidKit.Symbol, Is.EqualTo("+"));
        }
    }
}