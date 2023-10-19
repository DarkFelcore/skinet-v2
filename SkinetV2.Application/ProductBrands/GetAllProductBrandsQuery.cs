using ErrorOr;
using MediatR;
using SkinetV2.Domain.Products.ProductBrands;

namespace SkinetV2.Application.ProductBrands
{
    public record GetAllProductBrandsQuery() : IRequest<ErrorOr<List<ProductBrand>>>;
}