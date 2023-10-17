using ErrorOr;
using MediatR;
using SkinetV2.Domain.Products;

namespace SkinetV2.Application.Products.Get.ById
{
    public record GetProductQuery(
        string ProductId
    ) : IRequest<ErrorOr<Product>>;
}