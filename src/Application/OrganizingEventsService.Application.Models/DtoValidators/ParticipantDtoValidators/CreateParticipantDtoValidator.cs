using FluentValidation;
using OrganizingEventsService.Application.Models.Dto.Participant;

namespace OrganizingEventsService.Application.Models.DtoValidators.ParticipantDtoValidators;

public class CreateParticipantDtoValidator : AbstractValidator<CreateParticipantDto>
{
    public CreateParticipantDtoValidator()
    {
        RuleFor(dto => dto.AccountEmail)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Account email is invalid.");
        RuleFor(dto => dto.RoleId)
            .NotEmpty()
            .WithMessage("Role id can't be empty.");
    }
}