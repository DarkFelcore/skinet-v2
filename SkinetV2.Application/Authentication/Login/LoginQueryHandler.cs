using ErrorOr;
using MediatR;
using SkinetV2.Application.Authentication.Common;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Domain.Common.Errors;
using SkinetV2.Domain.Users;

namespace SkinetV2.Application.Authentication.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginQueryHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            // Check if the user email matches with any user's email in the database
            if((await _unitOfWork.UserRepository.GetUserByEmailAsync(query.Email)) is not User user)
            {
                return Errors.Users.InvalidCredentials;
            }

            // Check the input password with the password hash in the database
            if(!BCrypt.Net.BCrypt.Verify(query.Password, user.PasswordHash))
            {
                return Errors.Users.InvalidCredentials; 
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}