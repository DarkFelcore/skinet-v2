namespace SkinetV2.Contracts.Common
{
    public class Pagination<T>
        where T : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<T> Data { get; set; }
        public Pagination(int pageIndex, int pageSize, List<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Data = data;
        }
    }
}