using System;
using BookStore.Application.DTOs.Comments;
using FluentValidation;

namespace BookStore.Application.Validator.Comments;

public class CreateCommentDtoValidator : AbstractValidator<CreateCommentDto>
{
    public CreateCommentDtoValidator()
    {
        RuleFor(c => c.BookId)
            .NotEmpty().WithMessage("BookId is empty")
            .NotEqual(Guid.Empty).WithMessage("BookId is invalid");

        RuleFor(c => c.UserId)
            .NotEmpty().WithMessage("UserId is empty")
            .NotEqual(Guid.Empty).WithMessage("UserId is invalid");

        RuleFor(c => c.AuthorName)
            .NotEmpty().WithMessage("Author name is empty")
            .MaximumLength(100).WithMessage("The author name must be a maximum of 100 characters.");

        RuleFor(c => c.Content)
            .NotEmpty().WithMessage("Content is empty")
            .MaximumLength(2000).WithMessage("The content must be a maximum of 2000 characters.");
    }
}