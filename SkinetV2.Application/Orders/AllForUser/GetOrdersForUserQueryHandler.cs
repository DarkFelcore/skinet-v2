using ErrorOr;
using MediatR;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Application.common.Specifications;
using SkinetV2.Domain.Orders;

namespace SkinetV2.Application.Orders.AllForUser
{
    public class GetOrdersForUserQueryHandler : IRequestHandler<GetOrdersForUserQuery, ErrorOr<List<Order>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersForUserQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<List<Order>>> Handle(GetOrdersForUserQuery query, CancellationToken cancellationToken)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(query.BuyerEmail);
            return await _unitOfWork.OrderRepository.GetListAsync(spec);
        }
    }
}