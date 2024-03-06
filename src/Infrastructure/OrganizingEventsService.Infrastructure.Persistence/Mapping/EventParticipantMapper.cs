using OrganizingEventsService.Application.Models.Entities;
using OrganizingEventsService.Application.Models.Entities.Enums;
using OrganizingEventsService.Infrastructure.Persistence.Models;

namespace OrganizingEventsService.Infrastructure.Persistence.Mapping;

public static class EventParticipantMapper
{
    public static EventParticipant ToEntity(EventParticipantModel eventParticipantModel)
    {
        return new EventParticipant
        {
            Id = eventParticipantModel.Id,
            EventId = eventParticipantModel.EventId,
            AccountId = eventParticipantModel.AccountId,
            IsArchive = eventParticipantModel.IsArchive,
            IsBanned = eventParticipantModel.IsBanned,
            InviteStatus = (EventParticipantInviteStatus)eventParticipantModel.InviteStatus,
            RoleId = eventParticipantModel.RoleId
        };
    }

    public static EventParticipantModel ToModel(EventParticipant eventParticipant)
    {
        return new EventParticipantModel
        {
            Id = eventParticipant.Id,
            EventId = eventParticipant.EventId,
            AccountId = eventParticipant.AccountId,
            IsArchive = eventParticipant.IsArchive,
            IsBanned = eventParticipant.IsBanned,
            InviteStatus = (Models.Enums.EventParticipantInviteStatus)eventParticipant.InviteStatus,
            RoleId = eventParticipant.RoleId
        };
    }
}