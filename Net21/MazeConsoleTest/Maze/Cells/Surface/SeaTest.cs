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

        private Hero _hero;
        private Sea _sea;

        [SetUp]
        public void Setup()
        {
            _mazeMapMock = new Mock<IMazeMap>();

            _baseCharacterMock = new Mock<IBaseCharacter>();

            _sea = new Sea(1, 1, _mazeMapMock.Object);
            _hero = new Hero(2, 2, _mazeMapMock.Object);
        }

        [Test]
        public void TryStep_heroWithoutBoat()
        {
            Assert.That(_sea.TryStep(_hero) == false);
        }

        [Test]
        public void TryStep_heroWithBoat()
        {
            var boat = new Boat(3, 3, _mazeMapMock.Object, "boat");
            _hero.Inventory.Add(boat);
            Assert.That(_sea.TryStep(_hero) == true);
            _hero.Inventory.Remove(boat);
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
            Assert.That(_sea.TryStep(character.Object) == false);
        }
    }
}
