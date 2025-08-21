using System;
using BookStore.Application.DTOs.Categories;
using FluentValidation;

namespace BookStore.Application.Validator.Categories;

public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateDtoValidator()
    {
        RuleFor(c => c.Id).NotNull();

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