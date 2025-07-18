using MazeConsole.Maze;
using MazeConsole.Maze.Cells.Surface;
using MazeConsole.Maze.Cells.Сharacters;
using Moq;
using NUnit.Framework;

namespace MazeConsoleTest.Maze.Cells.Surface;

public class TeleportTest
{
    private Mock<IMazeMap> _mazeMapMock;
    private Mock<IBaseCharacter> _baseCharacterMock;
    private Teleport _teleport1;
    private Teleport _teleport2;

    [SetUp]
    public void Setup()
    {
        _mazeMapMock = new Mock<IMazeMap>();
        _baseCharacterMock = new Mock<IBaseCharacter>();
        _teleport1 = new Teleport(x:7, y:7, mazeMap:_mazeMapMock.Object);
        _teleport2 = new Teleport(x:14, y:14, mazeMap:_mazeMapMock.Object);
    }
    
    [Test]
    public void TryStep_ReturnTrue()
    {
        // Arrange
        _teleport1.Bind(_teleport2);

        //  Act
        var result = _teleport1.TryStep(_baseCharacterMock.Object);

        // Assert
        Assert.That(result == true, "Ooops! Teleport wasn't work!");
    }
    
    [Test]
    public void Bind_CheckThatTeleportBound()
    {
        //  Act
        _teleport1.Bind(_teleport2);

        // Assert
        Assert.That(_teleport1.IsBound, "Ooops! Teleport wasn't bound!");
        Assert.That(_teleport1.EndPoint == _teleport2, "Ooops! Teleport wasn't bound teleport2!");
    }
    
    [Test]
    public void TryStep_ThrowException()
    {
        //Act & Assert
        Assert.Throws<Exception>(
            () => _teleport1.TryStep(_baseCharacterMock.Object), 
            "Ooops! Teleport works without bind to other teleport.");
    }
}