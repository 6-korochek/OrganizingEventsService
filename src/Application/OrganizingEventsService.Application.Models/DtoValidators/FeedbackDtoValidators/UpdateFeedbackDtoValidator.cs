using FluentValidation;
using OrganizingEventsService.Application.Models.Dto.Feedback;

namespace OrganizingEventsService.Application.Models.DtoValidators.FeedbackDtoValidators;

public class UpdateFeedbackDtoValidator : AbstractValidator<UpdateFeedbackDto>
{
    public UpdateFeedbackDtoValidator()
    {
        RuleFor(dto => dto.Rating)
            .NotEmpty()
            .Must(rating => rating is >= 1 and <= 5)
            .WithMessage("Rating value must be between 1 and 5.");
        RuleFor(dto => dto.Text)
            .MaximumLength(2000)
            .WithMessage("Text is too long")
            .Matches("[^A-Za-zА-ЯЁа-яё0-9 ,.!@#$;%:&?*()_+=-]"); // ???
    }
}