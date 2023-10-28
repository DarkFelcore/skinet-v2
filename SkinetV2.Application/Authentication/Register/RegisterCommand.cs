using ErrorOr;
using MediatR;
using SkinetV2.Application.Authentication.Common;
using SkinetV2.Domain.Users.ValueObjects;

namespace SkinetV2.Application.Authentication.Register
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        Address Address
    ) : IRequest<ErrorOr<AuthenticationResult>>;
}