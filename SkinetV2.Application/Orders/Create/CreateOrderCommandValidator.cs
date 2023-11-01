using FluentValidation;

namespace SkinetV2.Application.Orders.Create
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.BasketId)
                .NotEmpty().WithMessage("BasketId is required.");
            RuleFor(x => x.DeliveryMethodId)
                .NotEmpty().WithMessage("DeliveryMethodId is required.");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.");
            RuleFor(x => x.Address.FirstName)
                .NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.Address.LastName)
                .NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.Address.Street)
                .NotEmpty().WithMessage("Street is required.");
            RuleFor(x => x.Address.PostalCode)
                .NotEmpty().WithMessage("PostalCode is required.");
            RuleFor(x => x.Address.Provice)
                .NotEmpty().WithMessage("Provice is required.");
            RuleFor(x => x.Address.City)
                .NotEmpty().WithMessage("City is required.");
            RuleFor(x => x.Address.Country)
                .NotEmpty().WithMessage("Country is required.");
        }
    }
}