using MazeConsole.Maze;
using MazeConsole.Maze.Cells.Сharacters;
using MazeConsole.Maze.Cells.Сharacters.Npcs;
using Moq;
using NUnit.Framework;

namespace MazeConsoleTest.Maze.Cells.Character.Npcs
{
    public class GoblinTest
    {
        private Mock<IMazeMap> _mazeMapMock;
        private Mock<IBaseCharacter> _baseCharacterMock;
        private Goblin _goblin;

        [SetUp]
        public void Setup()
        {
            _mazeMapMock = new Mock<IMazeMap>();// Stub
            _baseCharacterMock = new Mock<IBaseCharacter>();// Stub
            _goblin = new Goblin(15, 24, _mazeMapMock.Object, 2, 1); // Real

        }

        [Test]
        [TestCase(5, 0)]
        [TestCase(2, 0)]
        [TestCase(1, 0)]
        public void TryStep_DecreasesHeroHp_WhenGoblinAttacks(int heroInintialHp, int expectedHeroHp)
        {
            _baseCharacterMock.Object.Hp = heroInintialHp;

            var result = _goblin.TryStep(_baseCharacterMock.Object);

            Assert.That(_baseCharacterMock.Object.Hp, Is.EqualTo(expectedHeroHp));
            Assert.That(result, Is.False);
        }

        [Test]
        [TestCase(2, 1, 5, 1)]
        [TestCase(45, 52, 0, 1)]
        public void TryCheck_GoblinSymbolIsG(int x, int y, int hp, int money)
        {
            _goblin = new Goblin(x, y, _mazeMapMock.Object, hp, money);
            Assert.That(_goblin.Symbol, Is.EqualTo("g"));
        }

    }
}