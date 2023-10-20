using ErrorOr;
using MediatR;
using SkinetV2.Domain.Products;

namespace SkinetV2.Application.Products.Get.All
{
    public record GetAllProductsQuery(
        string? Sort,
        Guid? BrandId,
        Guid? TypeId,
        int Skip,
        int Take
    ) : IRequest<ErrorOr<List<Product>>>;
}