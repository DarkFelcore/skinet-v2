using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SkinetV2.Application.Products.Get.All;
using SkinetV2.Application.Products.Get.ById;
using SkinetV2.Contracts.Products;
using SkinetV2.Domain.Products;

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

        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var query = new GetAllProductsQuery();
            var result = await _mediator.Send(query);

            var mappedProducts = result.Value.Select(p => _mapper.Map<ProductResponse>(p)).ToList();

            return result.Match(
                result => Ok(mappedProducts),
                Problem
            );
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProductAsync(string id)
        {
            var query = new GetProductQuery(id);
            var result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<ProductResponse>(result)),
                Problem
            );
        }
    }
}