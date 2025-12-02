using System;
using BookStore.Application.DTOs.Carts;
using FluentValidation;

namespace BookStore.Application.Validator.Carts;

public class CartItemDtoValidator : AbstractValidator<CartItemDto>
{
    public CartItemDtoValidator()
    {
        RuleFor(i => i.BookId)
            .NotEmpty().WithMessage("BookId is required")
            .Must(id => id != Guid.Empty).WithMessage("BookId is invalid");

        RuleFor(i => i.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero");

        RuleFor(i => i.UnitPrice)
            .GreaterThanOrEqualTo(0).WithMessage("UnitPrice must be non-negative");

        RuleFor(i => i.TotalPrice)
            .Equal(i => i.Quantity * i.UnitPrice)
            .WithMessage("TotalPrice must be equal to Quantity * UnitPrice");
    }
}