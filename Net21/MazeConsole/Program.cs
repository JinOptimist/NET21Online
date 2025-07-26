using MazeConsole;

try
{
    var game = new ConsoleGameConroller();
    game.SwitchTypeOfGame();

}
catch (Exception ex)
{
    Console.Clear();
    Console.WriteLine("Maze borken. Sorry");
    throw ex;
}

