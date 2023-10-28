using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SkinetV2.Application.Baskets.Delete;
using SkinetV2.Application.Baskets.Get;
using SkinetV2.Application.Baskets.Update;
using SkinetV2.Contracts.Baskets;
using SkinetV2.Domain.Baskets;
namespace SkinetV2.Api.Controllers
{
    public class BasketController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _sender;

        public BasketController(IMapper mapper, ISender sender)
        {
            _mapper = mapper;
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasketById([FromQuery] GetBasketByIdRequest request)
        {
            var query = _mapper.Map<GetBasketByIdQuery>(request);
            var result = await _sender.Send(query);

            return result.Match(
                _ => Ok(result.Value),
                Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasketAsync(CustomerBasket request)
        {
            var command = _mapper.Map<UpdateBasketCommand>(request);
            var result = await _sender.Send(command);

            return result.Match(
                _ => Ok(result.Value),
                Problem
            );
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasketAsync([FromQuery] DeleteBasketRequest request)
        {
            var command = _mapper.Map<DeleteBasketCommand>(request);
            var result = await _sender.Send(command);

            return result.Match(
                result => NoContent(),
                Problem
            );
        }
    }
}