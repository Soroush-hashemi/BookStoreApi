using System.Linq;
using BookStore.Application.DTOs.Carts;
using FluentValidation;

namespace BookStore.Application.Validator.Carts;

public class CartDtoValidator : AbstractValidator<CartDto>
{
    public CartDtoValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty().WithMessage("UserId is required");

        RuleFor(c => c.Items)
            .NotNull().WithMessage("Cart items are required")
            .NotEmpty().WithMessage("Cart must contain at least one item");

        RuleForEach(c => c.Items).SetValidator(new CartItemDtoValidator());

        RuleFor(c => c.TotalItems)
            .Must((c, total) => c.Items != null && total == c.Items.Sum(i => i.Quantity))
            .WithMessage("TotalItems must equal the sum of item quantities")
            .When(c => c.Items != null);

        RuleFor(c => c.TotalPrice)
            .Must((c, total) => c.Items != null && total == c.Items.Sum(i => i.TotalPrice))
            .WithMessage("TotalPrice must equal the sum of item totals")
            .When(c => c.Items != null);
    }
}