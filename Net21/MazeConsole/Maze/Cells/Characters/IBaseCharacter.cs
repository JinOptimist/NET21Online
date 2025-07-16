namespace MazeConsole.Maze.Cells.Сharacters
{
    public interface IBaseCharacter : IBaseCell
    {
        int Hp { get; set; }
        int Money { get; set; }
    }
}