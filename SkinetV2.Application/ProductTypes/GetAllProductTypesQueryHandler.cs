using ErrorOr;
using MediatR;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Domain.Common.Errors;
using SkinetV2.Domain.Products.ProductTypes;

namespace SkinetV2.Application.ProductTypes
{
    public class GetAllProductTypesQueryHandler : IRequestHandler<GetAllProductTypesQuery, ErrorOr<List<ProductType>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductTypesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<List<ProductType>>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
        {
            var productTypes = await _unitOfWork.ProductRepository.GetAllProductTypesAsync();

            if(productTypes is null)
            {
                return Errors.ProductBrands.NotFound;
            }

            return productTypes;
        }
    }
}