using MazeCore.Maze.Cells;

namespace MazeCore.Builder
{
    public static class ExtentionToListForRandom
    {
        private static Random _random = new Random();

        public static CellType? GetRandomCell<CellType>(
            this IEnumerable<IBaseCell> list
            )
            where CellType : IBaseCell
        {
            var grounds = list
               .OfType<CellType>()
               .ToList();

            if (!grounds.Any())
            {
                return default;
            }
            var index = _random.Next(grounds.Count);
            return grounds[index];
        }
    }
}
