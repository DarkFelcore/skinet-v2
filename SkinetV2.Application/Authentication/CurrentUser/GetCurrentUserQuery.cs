using ErrorOr;
using MediatR;
using SkinetV2.Application.Authentication.Common;

namespace SkinetV2.Application.Authentication.CurrentUser
{
    public record GetCurrentUserQuery(string? Email) : IRequest<ErrorOr<AuthenticationResult>>;
}