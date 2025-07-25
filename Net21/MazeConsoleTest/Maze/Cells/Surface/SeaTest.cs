using MazeConsole.Maze;
using MazeConsole.Maze.Cells.Inventory;
using MazeConsole.Maze.Cells.Surface;
using MazeConsole.Maze.Cells.Сharacters;
using MazeConsole.Maze.Cells.Сharacters.Npcs;
using Moq;
using NUnit.Framework;

namespace MazeConsoleTest.Maze.Cells.Surface
{
    public class SeaTest
    {
        private Mock<IMazeMap> _mazeMapMock;

        private Mock<IBaseCharacter> _baseCharacterMock;

        private Mock<IHero> _hero;
        private Sea _sea;

        [SetUp]
        public void Setup()
        {
            _mazeMapMock = new Mock<IMazeMap>();

            _baseCharacterMock = new Mock<IBaseCharacter>();

            _sea = new Sea(1, 1, _mazeMapMock.Object);
            _hero = new Mock<IHero>();
            _hero.SetupAllProperties();
        }

        [Test]
        public void TryStep_heroWithoutBoat()
        {
            var inventory = new List<IBaseItems>();
            _hero.Setup(x => x.Inventory).Returns(inventory);

            var result = _sea.TryStep(_hero.Object);

            Assert.That(result == false);
        }

        [Test]
        public void TryStep_heroWithBoat()
        {
            var boat = new Mock<IBoat>();
            
            var inventory = new List<IBaseItems>() { boat.Object };
            _hero.Setup(x => x.Inventory).Returns(inventory);

            var resoult = _sea.TryStep(_hero.Object);

            Assert.That(resoult == true);
        }

        [Test]
        public void TryStep_npc() // This test is unnecessary, becouse enemis can't step on sea
        {
            // Sea.cs contains
            //
            // if (character is Goblin)
            // {
                // return true;
            // }
            // 
            // which is unneedble
            var character = new Mock<IBaseCharacter>();

            var result = _sea.TryStep(character.Object);

            Assert.That(result == false);
        }
    }
}
