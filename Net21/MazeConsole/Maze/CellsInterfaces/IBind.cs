namespace MazeConsole.Maze.CellsInterfaces;

/// <summary>
/// implements the ability to connect an object to another object
/// </summary>
/// <typeparam name="T">template or endpoint interface (most often IActive)</typeparam>
public interface IBind<T>
{
    T EndPoint { get; set; }
    bool IsBound { get; set; } //как устанвливать модификаторы доступа для полей интерфейса
    
    public void Bind(T endPoint);
}