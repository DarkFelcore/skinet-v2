using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkinetV2.Api.Extensions;
using SkinetV2.Application.DeliveryMethods.All;
using SkinetV2.Application.Orders.AllForUser;
using SkinetV2.Application.Orders.ById;
using SkinetV2.Application.Orders.Create;
using SkinetV2.Contracts.Orders;

namespace SkinetV2.Api.Controllers
{
    [Authorize]
    public class OrdersController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _sender;

        public OrdersController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet("deliverymethods")]
        public async Task<IActionResult> GetAllDeliveryMethodsAsync()
        {
            var query = new GetAllDeliveryMethodsQuery();
            var result = await _sender.Send(query);

            var mappedDeliveryMethods = result.Value.Select(x => _mapper.Map<DeliveryMethodResponse>(x)).ToList();

            return result.Match(
                result => Ok(mappedDeliveryMethods),
                Problem
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersForUserAsync()
        {
            var buyerEmail = AuthenticationExtensions.GetEmailByClaimTypesAsync(HttpContext.User);

            var query = new GetOrdersForUserQuery(buyerEmail);
            var result = await _sender.Send(query);

            var mappedOrders = result.Value.Select(x => _mapper.Map<OrderReponse>(x)).ToList();

            return result.Match(
                result => Ok(mappedOrders),
                Problem
            );
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOrderByIdAsync(Guid id)
        {
            var buyerEmail = AuthenticationExtensions.GetEmailByClaimTypesAsync(HttpContext.User);

            var query = new GetOrderByIdQuery(id, buyerEmail);
            var result = await _sender.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<OrderReponse>(result)),
                Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync(CreateOrderRequest request)
        {
            var buyerEmail = AuthenticationExtensions.GetEmailByClaimTypesAsync(HttpContext.User);

            var command = _mapper.Map<CreateOrderCommand>((buyerEmail, request));
            var result = await _sender.Send(command);

            return result.Match(
                _ => Ok(_mapper.Map<OrderReponse>(result.Value)),
                Problem
            );
        }
    }
}