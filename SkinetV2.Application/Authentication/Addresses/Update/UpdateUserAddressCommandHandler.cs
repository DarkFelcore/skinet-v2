using ErrorOr;
using MediatR;
using SkinetV2.Application.Authentication.Common;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Domain.Common.Errors;
using SkinetV2.Domain.Users.ValueObjects;

namespace SkinetV2.Application.Authentication.Addresses.Update
{
    public class UpdateUserAddressCommandHandler : IRequestHandler<UpdateUserAddressCommand, ErrorOr<AddressResult>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserAddressCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<AddressResult>> Handle(UpdateUserAddressCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(command.Email);

            if(user is null) 
            {
                return Errors.Users.InvalidCredentials;
            }

            var newAddress = new Address
            {
                Street = command.Street,
                PostalCode = command.PostalCode,
                Provice = command.Provice,
                City = command.City,
                Country = command.Country
            };

            user.Address = newAddress;

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            return new AddressResult(command.Street, command.PostalCode, command.Provice, command.City, command.Country);
        }
    }
}