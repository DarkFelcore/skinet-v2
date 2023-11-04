using ErrorOr;
using MediatR;
using SkinetV2.Application.Authentication.Common;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Domain.Common.Errors;

namespace SkinetV2.Application.Authentication.Addresses.Get
{
    public class GetUserAddressQueryHandler : IRequestHandler<GetUserAddressQuery, ErrorOr<AddressResult>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserAddressQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<AddressResult>> Handle(GetUserAddressQuery query, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(query.Email!);

            if (user is null) return Errors.Users.InvalidCredentials;

            if (user.Address is null) return new AddressResult(null, null, null, null, null);

            return new AddressResult(
                user.Address.Street,
                user.Address.PostalCode,
                user.Address.Provice,
                user.Address.City,
                user.Address.Country
            );
        }
    }
}