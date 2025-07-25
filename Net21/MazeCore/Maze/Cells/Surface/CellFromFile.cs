using MazeCore.Maze.Cells.Characters;

namespace MazeCore.Maze.Cells.Surface
{
    public class CellFromFile : BaseCell
    {
        public CellFromFile(int x, int y, IMazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public override string Symbol
        {
            get
            {
                var path = "D:\\1.txt";
                string mySymbol;

                FileStream fs = null;
                try
                {
                    fs = new FileStream(path, FileMode.Open);// LOCK file
                    var sr = new StreamReader(fs);
                    var symbol = sr.Read(); // EXCEPTION
                    return ((char)symbol).ToString();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    fs?.Dispose(); // UNLOCK File
                }
            }
        }

        public override bool TryStep(IBaseCharacter character)
        {
            throw new NotImplementedException();
        }
    }
}
