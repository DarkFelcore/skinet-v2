using ErrorOr;
using MediatR;

namespace SkinetV2.Application.Authentication.EmailExists
{
    public record CheckEmailExistsQuery(string Email) : IRequest<ErrorOr<bool>>;
}