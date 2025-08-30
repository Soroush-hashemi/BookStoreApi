using System;
using BookStore.Application.DTOs.Sliders;
using FluentValidation;

namespace BookStore.Application.Validator.Slider;

public class SliderDtoValidator : AbstractValidator<SliderDto>
{
    public SliderDtoValidator()
    {
        RuleFor(c => c.Link)
            .NotEmpty().WithMessage("Link is empty");

        RuleFor(c => c.Image)
            .NotEmpty().WithMessage("Image is empty");
    }
}