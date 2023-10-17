using ErrorOr;
using MediatR;
using SkinetV2.Domain.Products;

namespace SkinetV2.Application.Products.Get.All
{
    public record GetAllProductsQuery() : IRequest<ErrorOr<List<Product>>>;
}