using ErrorOr;
using MediatR;
using SkinetV2.Domain.Products.ProductTypes;

namespace SkinetV2.Application.ProductTypes
{
    public record GetAllProductTypesQuery() : IRequest<ErrorOr<List<ProductType>>>;
}