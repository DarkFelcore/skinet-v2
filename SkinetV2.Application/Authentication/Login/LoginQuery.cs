using ErrorOr;
using MediatR;
using SkinetV2.Application.Authentication.Common;

namespace SkinetV2.Application.Authentication.Login
{
    public record LoginQuery(
        string Email,
        string Password
    ) : IRequest<ErrorOr<AuthenticationResult>>;
}