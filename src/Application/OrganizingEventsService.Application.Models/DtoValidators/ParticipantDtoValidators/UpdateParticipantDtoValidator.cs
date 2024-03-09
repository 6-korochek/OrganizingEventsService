using FluentValidation;
using OrganizingEventsService.Application.Models.Dto.Participant;
using OrganizingEventsService.Application.Models.Entities.Enums;

namespace OrganizingEventsService.Application.Models.DtoValidators.ParticipantDtoValidators;

public class UpdateParticipantDtoValidator : AbstractValidator<UpdateParticipantDto>
{
    public UpdateParticipantDtoValidator()
    {
        RuleFor(dto => dto.InviteStatus)
            .Must(status => status is EventParticipantInviteStatus.Accepted or
                EventParticipantInviteStatus.Declined or EventParticipantInviteStatus.Pending)
            .WithMessage("Invite status is invalid.");
    }
}