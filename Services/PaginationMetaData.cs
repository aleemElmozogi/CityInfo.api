namespace CityInfo.api.Services
{
    public class PaginationMetaData
    {
        public int TotalItemsCount { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPagesCount { get; set; }
        public int PageSize { get; set; }

        public PaginationMetaData(int totalItemsCount, int currentPage, int pageSize)
        {
            TotalItemsCount = totalItemsCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPagesCount = (int)Math.Ceiling(totalItemsCount / (double)pageSize);
        }
    }
}
