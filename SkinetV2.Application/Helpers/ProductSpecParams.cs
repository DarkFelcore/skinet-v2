namespace SkinetV2.Application.Helpers
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;
        public int? PageIndex { get; set; } = 1;
        private int _pageSize = 6;
        public int? PageSize
        {
            get => _pageSize;
            set => _pageSize = ((int.IsNegative(value ?? MaxPageSize) || value > MaxPageSize) ? MaxPageSize : value) ?? MaxPageSize;
        }
        public string? Sort { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? TypeId { get; set; }

        private string _search;
        public string? Search 
        {
            get => _search;
            set => _search = value!.ToLower();
        }
    }
}