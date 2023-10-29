using ErrorOr;
using MediatR;
using SkinetV2.Application.common.Interfaces;

namespace SkinetV2.Application.Authentication.EmailExists
{
    public class CheckEmailExistsQueryHandler : IRequestHandler<CheckEmailExistsQuery, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckEmailExistsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(CheckEmailExistsQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.UserRepository.GetUserByEmailAsync(query.Email) is not null;
        }
    }
}