using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeConsole.Maze;
using MazeConsole.Maze.Cells.Surface;
using MazeConsole.Maze.Cells.Сharacters;
using Moq;
using NUnit.Framework;

namespace MazeConsoleTest.Maze.Cells.Surface
{
    public class IceTest
    {
        private Mock<IMazeMap> _mazeMapMock;
        private Mock<IBaseCharacter> _baseCharacterMock;
        private Hero _heroMock;
        private Ice _iceMock;
        private Ground _groundMock;



        [SetUp]
        public void Setup()
        {
            _mazeMapMock = new Mock<IMazeMap>();
            _baseCharacterMock = new Mock<IBaseCharacter>();
            _iceMock = new Ice(3, 4, _mazeMapMock.Object);
            _heroMock = new Hero(2, 3, _mazeMapMock.Object);
            _groundMock = new Ground(4, 5, _mazeMapMock.Object);


        }

        //[Test]
        //public void TryStep_CheckThatHeroChangeCoordinate()
        //{
        //    _baseCharacterMock.SetupProperty(cell => cell.X, 3);
        //    _baseCharacterMock.SetupProperty(cell => cell.Y, 4);
        //    _iceMock.TryStep(_baseCharacterMock.Object);

        //    Assert.That(_baseCharacterMock.Object.X, Is.EqualTo(4));
        //    Assert.That(_baseCharacterMock.Object.Y, Is.EqualTo(5));
        //}

        //[Test]
        //public void TryStep_HeroCanStepOnTheIce()
        //{
        //    var result = _iceMock.TryStep(_heroMock);
        //    Assert.That(result, Is.False, "Hero can't step on the Ice");
        //}

       
    }
}
