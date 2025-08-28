using WebPortal.Models.CompShop.Device;

namespace WebPortal.Models.CompShop
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Sort parametrs
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }


        // Потом будет удалено
        public List<DeviceViewModel> Items { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        private const int _startPageIndex = 1;

        public bool HasPreviousPage => PageIndex > _startPageIndex;

        public bool HasNextPage => PageIndex < TotalPages;

        public CategoryViewModel(List<DeviceViewModel> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }

        public static CategoryViewModel CreatePage(IEnumerable<DeviceViewModel> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new CategoryViewModel(items, count, pageIndex, pageSize);
        }
    }
}
