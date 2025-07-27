using MazeCore.Maze.Cells.Characters;

namespace MazeCore.Maze.Cells.Surface;

public class EntranceNextLevel : BaseCell, IEntrance
{
    public EntranceNextLevel(int x, int y, IMazeMap mazeMap) : base(x, y, mazeMap)
    {
    }

    public override string Symbol => "↓";

    public override bool TryStep(IBaseCharacter character)
    {
        if (character is Hero hero)
        {
            return TryEnter(hero);
        }

        return false;
    }

    public bool TryEnter(Hero hero)
    {
        if (MazeMap is MazeMap maze && maze.OnRequestNextLevel is not null)
        {
            maze.OnRequestNextLevel.Invoke();
        }

        return true;
    }
}