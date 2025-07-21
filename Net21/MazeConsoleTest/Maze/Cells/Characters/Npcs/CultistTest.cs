using MazeConsole.Maze;
using MazeConsole.Maze.Cells.Сharacters;
using MazeConsole.Maze.Cells.Сharacters.Npcs;
using Moq;
using NUnit.Framework;

namespace MazeConsoleTest.Maze.Cells.Characters.Npcs;

public class CultistTest
{
    private Mock<IMazeMap> _mazeMapMock;
    private Mock<IBaseCharacter> _baseCharacterMock;
    private Cultist _cultist;
    private EvilSpirit _evilSpirit;

    [SetUp]
    public void Setup()
    {
        _mazeMapMock = new Mock<IMazeMap>();
        _baseCharacterMock = new Mock<IBaseCharacter>();
        _cultist = new Cultist(x: 7, y: 7, mazeMap: _mazeMapMock.Object);
        _evilSpirit = new EvilSpirit(x: 14, y: 14, mazeMap: _mazeMapMock.Object);
    }

    [Test]
    public void TryStep_ReturnFalse()
    {
        //  Act
        var result = _cultist.TryStep(_baseCharacterMock.Object);

        // Assert
        Assert.That(!result, "Ooops! Cultist can't step to npc, except EvilSpirit");
    }

    [Test]
    public void TryStep_ReturnTrue()
    {
        //  Act
        var result = _cultist.TryStep(_evilSpirit);

        // Assert
        Assert.That(result, "Ooops! Cultist can't step to npc, except EvilSpirit");
    }
    
    [Test]
    public void CellToMove_CultistIsScaredFalse()
    {
        //  Act
        var result = _cultist.CellToMove();

        // Assert
        _mazeMapMock.Verify(maze => maze.GetNearCell(_cultist), Times.AtMost(2));
        Assert.That(result == null, "Ooops! Something wrong.");
    }
}