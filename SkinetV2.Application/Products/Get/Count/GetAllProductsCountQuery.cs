using ErrorOr;
using MediatR;

namespace SkinetV2.Application.Products.Get.Count
{
    public record GetAllProductsCountQuery(
        string? Search,
        Guid? BrandId,
        Guid? TypeId
    ) : IRequest<ErrorOr<int>>;
}