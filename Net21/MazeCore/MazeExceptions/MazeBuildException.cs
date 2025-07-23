namespace MazeCore.MazeExceptions
{
    public class MazeBuildException : Exception
    {
        public MazeBuildException()
        {
        }

        public MazeBuildException(string? message) : base(message)
        {
        }
    }
}
