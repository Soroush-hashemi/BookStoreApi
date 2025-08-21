using BookStore.Application.DTOs.Categories;
using FluentValidation;

namespace BookStore.Application.Validator.Categories;

public class CategoryDtoValidator : AbstractValidator<CategoryDto>
{
    public CategoryDtoValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("Category title is empty")
            .MaximumLength(200).WithMessage
            ("The Category title must be a maximum of 200 characters.");

        RuleFor(c => c.Slug)
            .NotEmpty().WithMessage("Category title is empty")
            .MaximumLength(200).WithMessage
            ("The Category title must be a maximum of 200 characters.");
    }
}
