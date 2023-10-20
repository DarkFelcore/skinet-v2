namespace SkinetV2.Application.Helpers
{
    public class ProductSearchParams
    {
        public string? Sort { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? TypeId { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public ProductSearchParams(string? sort, Guid? brandId, Guid? typeId, int? skip, int? take)
        {
            Sort = sort;
            BrandId = brandId;
            TypeId = typeId;
            Skip = skip ?? 0;
            Take = take ?? 50;
        }
    }
}