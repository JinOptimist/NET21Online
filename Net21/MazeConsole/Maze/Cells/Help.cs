namespace MazeConsole.Maze.Cells;

public class Help: BaseCell
{
    public Help(int x, int y, MazeMap mazeMap, string message) : base(x, y, mazeMap)
    {
        Message = message;
        mazeMap.ReplaceCell(this);
    }

    public override string Symbol { get; } = "H";

    private string Message { get; }

    public override bool TryStep(Hero hero)
    {
        if (hero.X != hero.PastX | hero.Y != hero.PastY)
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("                  Help                  ");
            Console.WriteLine("========================================");
            Console.WriteLine(Message);
            Console.WriteLine("========================================");
            Console.WriteLine("Press any key to close");
            Console.ReadKey();
        }
        
        return true;
    }
}