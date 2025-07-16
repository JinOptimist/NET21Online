using MazeConsole.Maze.Cells.Surface;

namespace MazeConsole.Maze.Cells.Сharacters.Npcs;

public class Wolf : BaseNpc
{
    public Wolf(int x, int y, MazeMap mazeMap, int hp = 1, int money = 1) : base(x, y, mazeMap)
    {
        Hp = hp;
        Money = money;
    }

    public override string Symbol => "(";
    public override bool TryStep(IBaseCharacter character)
    {
        if (character is Wolf)
        {
            return true;
        }

        character.Hp--;
        return false;
    }

    public override BaseCell? CellToMove()
    {
        var nearCell = MazeMap
            .GetNearCell(this);
        var hero = nearCell.OfType<Hero>().FirstOrDefault();
        if (hero != null)
        {
            return hero;
        }

        var grounds = MazeMap
            .GetNearCell(this)
            .OfType<Ground>();
        var enumerable = grounds.ToList();
        return !enumerable.Any() ? null : enumerable.First();
    }
}