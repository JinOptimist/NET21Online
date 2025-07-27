using MazeCore.Maze.Cells.Characters;

namespace MazeCore.Maze.Cells.Surface;

public class EntrancePrevLevel : BaseCell, IEntrance
{
    public EntrancePrevLevel(int x, int y, IMazeMap mazeMap) : base(x, y, mazeMap)
    {
    }

    public override string Symbol => "↑";

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
        if (MazeMap is MazeMap maze && maze.OnRequestPrevLevel is not null)
        {
            maze.OnRequestPrevLevel.Invoke();
        }

        return true;
    }
}