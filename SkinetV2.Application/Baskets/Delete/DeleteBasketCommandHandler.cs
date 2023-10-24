using ErrorOr;
using MediatR;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Domain.Common.Errors;

namespace SkinetV2.Application.Baskets.Delete
{
    public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand, ErrorOr<bool>>
    {
        private readonly IBasketRepository _basketRepository;

        public DeleteBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<ErrorOr<bool>> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            var result = await _basketRepository.DeleteBasketAsync(command.BasketId);

            if(!result) return Errors.Basket.DeleteFailed;

            return result;
        }
    }
}