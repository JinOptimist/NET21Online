using MazeConsole.Maze.Cells.Ñharacters;

namespace MazeConsole.Maze.Cells.Surface;

/// <summary>
/// causes the player to slide to the opposite bank if the player steps on the ice;
/// deals damage if the player crashes into a wall;
/// ignored if player has "snowstepers"
/// Usage: placed like a normal cell
/// </summary>
public class Ice : BaseCell
{
    public Ice(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
    {
        mazeMap.ReplaceCell(this);
    }

    public override string Symbol => "%";

    public override bool TryStep(BaseCharacter character)
    {
        var vectorX = X - character.X;
        var vectorY = Y - character.Y;

        var indexX = character.X;
        var indexY = character.Y;

        var isOnIce = true;
        var damage = 0;

        while (isOnIce)
        {
            indexX += vectorX;
            indexY += vectorY;

            var cell = MazeMap[indexX, indexY];

            if(cell == null)
            {
                return false;
            }

            if (cell.Symbol != Symbol)
            {
                isOnIce = false;
                if (!cell.TryStep(character))
                {
                    indexX -= vectorX;
                    indexY -= vectorY;
                    character.Hp -= damage;
                }
            }
            damage += 1;
        }

        character.X = indexX;
        character.Y = indexY;

        return false;
    }
}