using ErrorOr;
using MediatR;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Application.common.Specifications;
using SkinetV2.Domain.Common.Errors;
using SkinetV2.Domain.Orders;
using SkinetV2.Domain.Orders.ValueObjects;

namespace SkinetV2.Application.Orders.ById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, ErrorOr<Order>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Order>> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            var orderId = new OrderId(query.OrderId);
            var spec = new OrdersWithItemsAndOrderingSpecification(orderId, query.BuyerEmail);
            var order = await _unitOfWork.OrderRepository.GetEntityWithSpec(spec);

            if(order is null)
            {
                return Errors.Order.NotFound;
            }

            return order;
        }
    }
}