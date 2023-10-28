using FluentValidation;

namespace SkinetV2.Application.Authentication.Addresses.Update
{
    public class UpdateUserAddressCommandValidator : AbstractValidator<UpdateUserAddressCommand>
    {
        public UpdateUserAddressCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address is required");
            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street is required");
            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage("PostalCode is required");
            RuleFor(x => x.Provice)
                .NotEmpty().WithMessage("Provice is required");
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required");
            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required");
        }
    }
}