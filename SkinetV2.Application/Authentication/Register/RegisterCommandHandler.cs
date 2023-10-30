using ErrorOr;
using MediatR;
using SkinetV2.Application.Authentication.Common;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Domain.Common.Errors;
using SkinetV2.Domain.Users;
using SkinetV2.Domain.Users.ValueObjects;

namespace SkinetV2.Application.Authentication.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public RegisterCommandHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if ((await _unitOfWork.UserRepository.GetUserByEmailAsync(command.Email)) is not null)
            {
                return Errors.Users.DuplicateEmailAddress;
            }

            // Hash the password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);

            // Creating new user obejct
            var user = new User
            {
                UserId = new UserId(Guid.NewGuid()),
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                PasswordHash = passwordHash,
                // If the no address is specified, set all the addresss properties to null
                // If only one address property is specified, set all the addresses properties to null
                // If all the addresses properties are specified, set the address for the user
                Address = command.Address is not null 
                    ? command.Address!.Street is not null && command.Address!.PostalCode is not null && command.Address!.Provice is not null && command.Address!.City is not null && command.Address!.Country is not null
                         ? new Address(command.Address!.Street, command.Address.PostalCode, command.Address.Provice, command.Address.City, command.Address.Country)
                         : null
                    : null
            };

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }
    }
}