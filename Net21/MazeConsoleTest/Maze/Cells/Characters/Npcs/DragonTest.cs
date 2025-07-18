using MazeConsole.Maze;
using MazeConsole.Maze.Cells.Surface;
using MazeConsole.Maze.Cells.Сharacters;
using MazeConsole.Maze.Cells.Сharacters.Npcs;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsoleTest.Maze.Cells.Characters.Npcs
{
    public class DragonTest
    {
        private Mock<IMazeMap> _mazeMapMock;
        private Mock<IBaseCharacter> _baseCharacterMock;
        private Dragon _dragon;

        [SetUp]
        public void Setup()
        {
            _mazeMapMock = new Mock<IMazeMap>();
            _baseCharacterMock = new Mock<IBaseCharacter>();
            _dragon = new Dragon(10, 10, _mazeMapMock.Object, 10, 10);
        }

        [Test]
        public void TryStep_DragonRetutnTrue()
        {
            Assert.That(_dragon.TryStep(_dragon), "Dragon can't step on the Dragon. It's bad!");
        }

        [Test]//HELP
        public void TryStep_NotDragonRetutnFalse()
        {
            //HELP
            Assert.That(!_dragon.TryStep(_baseCharacterMock.Object), "notDragon can step on the Dragon. Is a bad!"); //Как лучше написать?
            //HELP
        }

        [Test]
        [TestCase(3, 0)]
        [TestCase(10, 7)]
        [TestCase(1000, 997)]
        public void TryStep_CheckThatHeroHpIsIncrease(int hpStart, int hpResult)
        {
            var heroMock = new Mock<IHero>();
            heroMock.SetupAllProperties();

            var hero = heroMock.Object;

            hero.Hp = hpStart;

            _dragon.TryStep(hero);

            Assert.That(hero.Hp == hpResult, $"Hp is not {hpResult}. It's a problem!");
        }

        [Test]
        [TestCase(2, 1)]
        [TestCase(12, 11)]
        [TestCase(1000, 999)]
        public void TryStep_CheckThatDragonHpIsIncrease(int hpStart, int hpResult)
        {
            var hero = new Mock<IHero>().SetupAllProperties().Object;

            _dragon.Hp = hpStart;

            _dragon.TryStep(hero);

            Assert.That(_dragon.Hp == hpResult, $"Hp Dragon's not {hpResult}. It's a problem!");
        }

        [Test] //HELP
        public void TryStep_RemoveNpcWhenDragonHas0Hp()
        {
            //HELP
            _dragon.Hp = 0;
            _mazeMapMock.Verify(maze => maze.Npcs.Remove(_dragon), Times.Once); //Ошибка при удалении _dragon в _dragon.TryStep()
            //HELP
        }

        [Test] //HELP
        public void TryStep_CellWasReplacedOnCoin()
        {
            //HELP
            _dragon.Hp = 0;
            _dragon.TryStep(_baseCharacterMock.Object);

            _mazeMapMock.Verify(maze => maze.ReplaceCell(It.IsAny<Coin>()), Times.Once); //Ошибка при удалении _dragon в _dragon.TryStep()
            //HELP
        }

        // Какие тесты писать к  CellToMove()?
    }
}
