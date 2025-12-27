using FluentValidation;

namespace MicroserviceProject.Order.Application.Features.Orders.Create
{
    public class CreateOrderCommandValidator
     : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Address)
                .NotNull().WithMessage("Address is required.")
                .SetValidator(new AddressDtoValidator());

            RuleFor(x => x.Payment)
                .NotNull().WithMessage("Payment information is required.")
                .SetValidator(new PaymentDtoValidator());

            RuleFor(x => x.OrderItems)
                .NotNull().WithMessage("Order items are required.")
                .NotEmpty().WithMessage("At least one order item must be provided.");

            RuleForEach(x => x.OrderItems)
                .SetValidator(new OrderItemDtoValidator());

            RuleFor(x => x.DiscountRate)
                .InclusiveBetween(0, 100)
                .When(x => x.DiscountRate.HasValue)
                .WithMessage("Discount rate must be between 0 and 100.");
        }
    }

    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(x => x.Line)
                .NotEmpty().WithMessage("Address line is required.")
                .MaximumLength(250).WithMessage("Address line must not exceed 250 characters.");

            RuleFor(x => x.Province)
                .NotEmpty().WithMessage("Province is required.")
                .MaximumLength(100).WithMessage("Province must not exceed 100 characters.");

            RuleFor(x => x.District)
                .NotEmpty().WithMessage("District is required.")
                .MaximumLength(100).WithMessage("District must not exceed 100 characters.");

            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street is required.")
                .MaximumLength(150).WithMessage("Street must not exceed 150 characters.");

            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage("Zip code is required.")
                .Matches(@"^\d{5}$").WithMessage("Zip code must be exactly 5 digits.");
        }
    }

    public class PaymentDtoValidator : AbstractValidator<PaymentDto>
    {
        public PaymentDtoValidator()
        {
            RuleFor(x => x.CardHolderName)
                .NotEmpty().WithMessage("Card holder name is required.")
                .MaximumLength(150).WithMessage("Card holder name must not exceed 150 characters.");

            RuleFor(x => x.CardNumber)
                .NotEmpty().WithMessage("Card number is required.")
                .CreditCard().WithMessage("Card number must be a valid credit card number.");

            RuleFor(x => x.Expiration)
                .NotEmpty().WithMessage("Expiration date is required.")
                .Matches(@"^(0[1-9]|1[0-2])\/\d{2}$")
                .WithMessage("Expiration date must be in MM/YY format.");

            RuleFor(x => x.Cvc)
                .NotEmpty().WithMessage("CVC is required.")
                .Matches(@"^\d{3,4}$").WithMessage("CVC must be 3 or 4 digits.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Payment amount must be greater than zero.");
        }
    }


    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Product id is required.");

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(200).WithMessage("Product name must not exceed 200 characters.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than zero.");
        }
    }
}
