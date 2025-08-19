using BookStore.Application.DTOs.Books;
using FluentValidation;

namespace BookStore.Application.Validator.Books;

public class BookUpdateDtoValidator : AbstractValidator<BookUpdateDto>
{
    public BookUpdateDtoValidator()
    {
        RuleFor(b => b.Id).NotNull();

        RuleFor(b => b.Title)
            .NotEmpty().WithMessage("Book title is empty")
            .MaximumLength(200).WithMessage
            ("The book title must be a maximum of 200 characters.");

        RuleFor(b => b.Slug)
            .NotEmpty().WithMessage("Book Slug is empty")
            .MaximumLength(200).WithMessage
            ("The Slug must be a maximum of 200 characters.")
            .MinimumLength(7).WithMessage
            ("The Slug must be a Minimum of 200 characters.");

        RuleFor(b => b.Author)
            .NotEmpty().WithMessage("Author is required")
            .MaximumLength(150).WithMessage("Author name must be a maximum of 150 characters.");

        RuleFor(b => b.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(1000).WithMessage("Description must be a maximum of 1000 characters.");

        RuleFor(b => b.Price)
            .NotNull().WithMessage("Price is required")
            .GreaterThanOrEqualTo(0).WithMessage("Price must be non-negative.");

        RuleFor(b => b.CategoryId)
            .NotNull().WithMessage("Category is required");
    }
}