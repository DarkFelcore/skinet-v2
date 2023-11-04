using ErrorOr;
using MediatR;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Application.common.Specifications;
using SkinetV2.Domain.Orders.Entities;

namespace SkinetV2.Application.DeliveryMethods.All
{
    public class GetAllDeliveryMethodsQueryHandler : IRequestHandler<GetAllDeliveryMethodsQuery, ErrorOr<List<DeliveryMethod>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllDeliveryMethodsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<List<DeliveryMethod>>> Handle(GetAllDeliveryMethodsQuery query, CancellationToken cancellationToken)
        {
            var spec = new DeliveryMethodsWithPriceDescSpecification();
            return await _unitOfWork.DeliveryMethodRepository.GetListAsync(spec);
        }
    }
}