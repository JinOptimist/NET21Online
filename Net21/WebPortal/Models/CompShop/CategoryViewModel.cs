using WebPortal.DbStuff.Models.CompShop;
using WebPortal.DbStuff.Models.CompShop.Devices;

namespace WebPortal.Models.CompShop
{
    public class CategoryViewModel
    {
        public List<BaseDevice> Items { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        private const int _startPageIndex = 1;

        public bool HasPreviousPage => PageIndex > _startPageIndex;

        public bool HasNextPage => PageIndex < TotalPages;

        //Sort parametrs
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }

        public CategoryViewModel(List<BaseDevice> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }

        public static CategoryViewModel CreatePage(IEnumerable<BaseDevice> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new CategoryViewModel(items, count, pageIndex, pageSize);
        }
    }
}
