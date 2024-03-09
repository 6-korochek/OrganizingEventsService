using FluentValidation;
using OrganizingEventsService.Application.Models.Dto.Event;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Models.DtoValidators.EventDtoValidators;

public class UpdateEventDtoValidator : AbstractValidator<UpdateEventDto>
{
    public UpdateEventDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .MaximumLength(100)
            .WithMessage("This name is too long.")
            .Must(c => c.All(char.IsLetter));
        RuleFor(dto => dto.StartDate)
            .GreaterThan(new DateTime(2024, 1, 1))
            .WithMessage("Start date is too early.")
            .LessThan(new DateTime(2030, 1, 1))
            .WithMessage("Start date is too late.");
        RuleFor(dto => dto.EndDate)
            .GreaterThan(dto => dto.StartDate)
            .WithMessage("End date must be later than StartDate")
            .LessThan(new DateTime(2030, 1, 1))
            .WithMessage("End date is too late.");
        RuleFor(dto => dto.Description)
            .Matches("[^A-Za-zА-ЯЁа-яё0-9 ,.!@#$;%:&?*()_+=-]");  // ???
        RuleFor(dto => dto.MeetingLink)
            .MinimumLength(8)
            .WithMessage("Meeting link is too short.")
            .MaximumLength(1000)
            .WithMessage("Meeting link is too long.")
            .Matches("[^A-Za-zА-ЯЁа-яё0-9 ,.!@#$;%:&?*()_+=-]");
        RuleFor(dto => dto.MaxParticipant)
            .GreaterThan((uint)1)
            .WithMessage("Value of max participants must be greater than 1.")
            .LessThan((uint)500)
            .WithMessage("Value of max participants must be less than 500.");
        RuleFor(dto => dto.EventStatus)
            .Must(status => status is EventStatus.Planed or EventStatus.InDraft or EventStatus.InGoing or EventStatus.IsOver)
            .WithMessage("Event status is invalid.");
    }
}