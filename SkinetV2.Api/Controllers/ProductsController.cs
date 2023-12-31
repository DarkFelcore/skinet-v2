using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SkinetV2.Application.ProductBrands;
using SkinetV2.Application.Products.Get.All;
using SkinetV2.Application.Products.Get.ById;
using SkinetV2.Application.ProductTypes;
using SkinetV2.Contracts.ProductBrands;
using SkinetV2.Contracts.Products;
using SkinetV2.Contracts.ProductTypes;
using SkinetV2.Application.Helpers;
using SkinetV2.Contracts.Common;
using SkinetV2.Application.Products.Get.Count;
using SkinetV2.Api.Common.Helpers;

namespace SkinetV2.Api.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public ProductsController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Cached(600)]
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] ProductSpecParams productSpecParams)
        {
            var query = _mapper.Map<GetAllProductsQuery>(productSpecParams);
            var result = await _mediator.Send(query);

            var mappedProducts = result.Value.Select(p => _mapper.Map<ProductResponse>(p)).ToList();

            var queryCount = _mapper.Map<GetAllProductsCountQuery>(productSpecParams);
            var resultCount = await _mediator.Send(queryCount);

            return result.Match(
                result => Ok(new Pagination<ProductResponse>(productSpecParams.PageIndex ?? 1, productSpecParams.PageSize ?? 6, resultCount.Value, mappedProducts)),
                Problem
            );
        }

        [HttpGet("{id:guid}")]
        [Cached(600)]
        public async Task<IActionResult> GetProductAsync(string id)
        {
            var query = new GetProductQuery(id);
            var result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<ProductResponse>(result)),
                Problem
            );
        }

        [Cached(600)]
        [HttpGet("brands")]
        public async Task<IActionResult> GetAllProductBrandsAsync()
        {
            var query = new GetAllProductBrandsQuery();
            var result = await _mediator.Send(query);

            var mappedProductBrands = result.Value.Select(pb => _mapper.Map<ProductBrandsReponse>(pb)).ToList();

            return result.Match(
                result => Ok(mappedProductBrands),
                Problem
            );
        }

        [Cached(600)]
        [HttpGet("types")]
        public async Task<IActionResult> GetAllProductTypesAsync()
        {
            var query = new GetAllProductTypesQuery();
            var result = await _mediator.Send(query);

            var mappedProductTypes = result.Value.Select(pt => _mapper.Map<ProductTypesReponse>(pt)).ToList();

            return result.Match(
                result => Ok(mappedProductTypes),
                Problem
            );
        }
    }
}