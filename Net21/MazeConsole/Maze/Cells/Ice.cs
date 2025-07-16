using MazeConsole.Maze.Cells.Сharacters;

namespace MazeConsole.Maze.Cells;

/// <summary>
/// causes the player to slide to the opposite bank if the player steps on the ice;
/// deals damage if the player crashes into a wall;
/// ignored if player has "snowstepers"
/// Usage: placed like a normal cell
/// </summary>
public class Ice: BaseCell
{
    public Ice(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
    {
        mazeMap.ReplaceCell(this);
    }

    public override string Symbol => "%";

    public override bool TryStep(BaseCharacter hero)
    {
        var vectorX = X - hero.X;
        var vectorY = Y - hero.Y;

        var indexX = hero.X;
        var indexY = hero.Y;

        var isOnIce = true;
        var damage = 0;

        while (isOnIce)
        {
            indexX += vectorX;
            indexY += vectorY;
            

            if (MazeMap[indexX, indexY].Symbol != Symbol)
            {
                isOnIce = false;
                if (!MazeMap[indexX, indexY].TryStep(hero))
                {
                    indexX -= vectorX;
                    indexY -= vectorY;
                    hero.Hp -= damage;
                }
            }
            damage += 1;
        }

        hero.X = indexX;
        hero.Y = indexY;

        return false;
    }
}