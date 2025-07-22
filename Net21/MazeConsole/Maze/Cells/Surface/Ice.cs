using MazeConsole.Maze.Cells.Сharacters;

namespace MazeConsole.Maze.Cells.Surface;

/// <summary>
/// causes the player to slide to the opposite bank if the player steps on the ice;
/// deals damage if the player crashes into a wall;
/// ignored if player has "snowstepers"
/// Usage: placed like a normal cell
/// </summary>
public class Ice : BaseCell
{
    public Ice(int x, int y, IMazeMap mazeMap) : base(x, y, mazeMap)
    {
        mazeMap.ReplaceCell(this);
    }


    public override string Symbol => "%";

    public override bool TryStep(IBaseCharacter character)
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


            if (MazeMap[indexX, indexY].Symbol != Symbol)
            {
                isOnIce = false;
                if (!MazeMap[indexX, indexY].TryStep(character))
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