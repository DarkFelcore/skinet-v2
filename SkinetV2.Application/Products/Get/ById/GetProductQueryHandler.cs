using ErrorOr;
using MediatR;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Domain.Common.Errors;
using SkinetV2.Domain.Products;
using SkinetV2.Domain.Products.ValueObjects;
using SkinetV2.Infrastructure.Persistance.Specifications;

namespace SkinetV2.Application.Products.Get.ById
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ErrorOr<Product>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Product>> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            var productId = new ProductId(Guid.Parse(query.ProductId));
            var spec = new ProductsWithBrandsAndTypesSpecification(productId);
            var product = await _unitOfWork.ProductRepository.GetEntityWithSpec(spec);

            if(product is null)
            {
                return Errors.Product.NotFound;
            }

            return product;
        }
    }
}