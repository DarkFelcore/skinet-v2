using ErrorOr;
using MediatR;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Domain.Common.Errors;
using SkinetV2.Domain.Products;
using SkinetV2.Infrastructure.Persistance.Specifications;

namespace SkinetV2.Application.Products.Get.All
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ErrorOr<List<Product>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<List<Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var spec = new ProductsWithBrandsAndTypesSpecification();
            var products = await _unitOfWork.ProductRepository.GetListAsync(spec);
            
            if(products is null)
            {
                return Errors.Product.NotFound;
            }

            return products;
        }
    }
}