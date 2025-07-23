using MazeConsole.Maze;
using MazeConsole.Maze.Cells.Surface;
using MazeConsole.Maze.Cells.Сharacters;
using MazeConsole.Maze.Cells.Сharacters.Npcs;
using Moq;
using NUnit.Framework;

namespace MazeConsoleTest.Maze.Cells.Npc
{
    public class WolfTest
    {
        private Mock<IMazeMap> _mazeMapMock;
        private Mock<IBaseCharacter> _baseCharacterMock;
        private Wolf _wolf;

        [SetUp]
        public void Setup()
        {
            _mazeMapMock = new Mock<IMazeMap>();// Stub
            _baseCharacterMock = new Mock<IBaseCharacter>();// Stub
            _wolf = new Wolf(1, 1, _mazeMapMock.Object); // Real
            _wolf.Hp = 10;
        }

        [Test]
        public void TryStep_Wolf()
        {
            var subWolf = new Wolf(2, 2, _mazeMapMock.Object);
            Assert.That(_wolf.TryStep(subWolf) == true);
        }

        [Test]
        public void TryStep_Hero()
        {
            var subHero = new Mock<IHero>();
            subHero.SetupAllProperties();
            var startHp = 10;
            subHero.Object.Hp = startHp;

            var tryStepResoult = _wolf.TryStep(subHero.Object);

            Assert.That(tryStepResoult == false && subHero.Object.Hp < startHp);
        }
    }
}
