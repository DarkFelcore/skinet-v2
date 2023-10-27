using ErrorOr;
using MediatR;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Application.common.Specifications;
using SkinetV2.Domain.Products.ProductBrands.ValueObjects;
using SkinetV2.Domain.Products.ProductTypes.ValueObjects;

namespace SkinetV2.Application.Products.Get.Count
{
    public class GetAllProductsCountQueryHandler : IRequestHandler<GetAllProductsCountQuery, ErrorOr<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductsCountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<int>> Handle(GetAllProductsCountQuery query, CancellationToken cancellationToken)
        {
            var productBrandId = new ProductBrandId(query.BrandId);
            var productTypeId = new ProductTypeId(query.TypeId);
            var spec = new ProductWithFiltersForCountSpecification(productBrandId, productTypeId, query.Search!);
            return await _unitOfWork.ProductRepository.CountAsync(spec);
        }
    }
}