using ErrorOr;
using MediatR;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Domain.Common.Errors;
using SkinetV2.Domain.Products.ProductBrands;

namespace SkinetV2.Application.ProductBrands
{
    public class GetAllProductBrandsQueryHandler : IRequestHandler<GetAllProductBrandsQuery, ErrorOr<List<ProductBrand>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductBrandsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<List<ProductBrand>>> Handle(GetAllProductBrandsQuery request, CancellationToken cancellationToken)
        {
            var productBrands = await _unitOfWork.ProductRepository.GetAllProductBrandsAsync();

            if(productBrands is null)
            {
                return Errors.ProductTypes.NotFound;
            }

            return productBrands;
        }
    }
}