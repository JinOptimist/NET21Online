using MazeConsole;

try
{
    var game = new ConsoleGameConroller();
    game.Play();
}
catch (Exception)
{
    Console.Clear();
    Console.WriteLine("Maze borken. Sorry");
}

