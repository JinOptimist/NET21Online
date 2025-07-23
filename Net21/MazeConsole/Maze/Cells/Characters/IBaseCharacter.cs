namespace MazeConsole.Maze.Cells.Characters
{
    public interface IBaseCharacter : IBaseCell
    {
        int Hp { get; set; }
        int Money { get; set; }
    }
}