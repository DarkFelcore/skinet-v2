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
            if((await _unitOfWork.UserRepository.GetUserByEmailAsync(command.Email)) is not null)
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
                Address = new Address
                {
                    Street = command.Address.Street,
                    PostalCode = command.Address.PostalCode,
                    Provice = command.Address.Provice,
                    City = command.Address.City,
                    Country = command.Address.Country,
                }
            };

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }
    }
}