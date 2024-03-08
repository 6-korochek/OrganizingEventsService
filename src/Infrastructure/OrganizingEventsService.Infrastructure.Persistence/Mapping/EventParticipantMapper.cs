using OrganizingEventsService.Application.Models.Entities;
using OrganizingEventsService.Application.Models.Entities.Enums;
using OrganizingEventsService.Infrastructure.Persistence.Models;

namespace OrganizingEventsService.Infrastructure.Persistence.Mapping;

public static class EventParticipantMapper
{
    public static EventParticipant ToEntity(EventParticipantModel eventParticipantModel)
    {
        var entity = new EventParticipant
        {
            Id = eventParticipantModel.Id,
            EventId = eventParticipantModel.EventId,
            AccountId = eventParticipantModel.AccountId,
            IsArchive = eventParticipantModel.IsArchive,
            IsBanned = eventParticipantModel.IsBanned,
            InviteStatus = (EventParticipantInviteStatus)eventParticipantModel.InviteStatus,
            RoleId = eventParticipantModel.RoleId
        };

        if (eventParticipantModel.AccountIdNavigation is not null)
        {
            entity.AccountIdNavigation = AccountMapper.ToEntity(eventParticipantModel.AccountIdNavigation);
        }

        if (eventParticipantModel.RoleIdNavigation is not null)
        {
            entity.RoleIdNavigation = new Role
            {
                Id = eventParticipantModel.RoleIdNavigation.Id,
                Name = eventParticipantModel.RoleIdNavigation.Name
            };
        }

        return entity;
    }

    public static EventParticipantModel ToModel(EventParticipant eventParticipant)
    {
        var model = new EventParticipantModel
        {
            Id = eventParticipant.Id,
            EventId = eventParticipant.EventId,
            AccountId = eventParticipant.AccountId,
            IsArchive = eventParticipant.IsArchive,
            IsBanned = eventParticipant.IsBanned,
            InviteStatus = (Models.Enums.EventParticipantInviteStatus)eventParticipant.InviteStatus,
            RoleId = eventParticipant.RoleId
        };
            
        if (eventParticipant.AccountIdNavigation is not null)
        {
            model.AccountIdNavigation = AccountMapper.ToModel(eventParticipant.AccountIdNavigation);
        }

        if (eventParticipant.RoleIdNavigation is not null)
        {
            model.RoleIdNavigation = new RoleModel
            {
                Id = eventParticipant.RoleIdNavigation.Id,
                Name = eventParticipant.RoleIdNavigation.Name
            };
        }

        return model;
    }
}