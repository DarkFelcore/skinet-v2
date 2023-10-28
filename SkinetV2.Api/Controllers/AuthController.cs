using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkinetV2.Api.Extensions;
using SkinetV2.Application.Authentication.Addresses.Get;
using SkinetV2.Application.Authentication.Addresses.Update;
using SkinetV2.Application.Authentication.CurrentUser;
using SkinetV2.Application.Authentication.Login;
using SkinetV2.Application.Authentication.Register;
using SkinetV2.Contracts.Authentication;
using SkinetV2.Contracts.Authentication.Common;

namespace SkinetV2.Api.Controllers
{
    public class AuthController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public AuthController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserAsync()
        {
            var email = AuthenticationExtensions.GetEmailByClaimTypesAsync(HttpContext.User);

            var query = new GetCurrentUserQuery(email);
            var result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<AuthenticationResponse>(result)),
                Problem
            );
        }

        [HttpGet("address")]
        [Authorize]
        public async Task<IActionResult> GetUserAddressAsync()
        {
            var email = AuthenticationExtensions.GetEmailByClaimTypesAsync(HttpContext.User);

            var query = new GetUserAddressQuery(email);
            var result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<AddressResponse>(result)),
                Problem
            );
        }

        [HttpPut("address")]
        [Authorize]
        public async Task<IActionResult> UpdateUserAddressAsync(UpdateUserAddressRequest request)
        {
            var email = AuthenticationExtensions.GetEmailByClaimTypesAsync(HttpContext.User);

            var command = _mapper.Map<UpdateUserAddressCommand>((email, request));
            var result = await _mediator.Send(command);

            return result.Match(
                result => Ok(_mapper.Map<AddressResponse>(result)),
                Problem
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            var result = await _mediator.Send(command);

            return result.Match(
                result => Ok(_mapper.Map<AuthenticationResponse>(result)),
                Problem
            );
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);
            var result = await _mediator.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<AuthenticationResponse>(result)),
                Problem
            );
        }
    }
}