using OrganizingEventsService.Application.Models.Entities;
using OrganizingEventsService.Application.Models.Entities.Enums;
using OrganizingEventsService.Infrastructure.Persistence.Models;

namespace OrganizingEventsService.Infrastructure.Persistence.Mapping;

public static class EventMapper
{
    public static Event ToEntity(EventModel eventModel)
    {
        Event entity = new Event
        {
            Id = eventModel.Id,
            Name = eventModel.Name,
            Description = eventModel.Description,
            Status = (EventStatus)eventModel.Status,
            EndDatetime = eventModel.EndDatetime,
            InviteCode = eventModel.InviteCode,
            MaxParticipant = eventModel.MaxParticipant,
            MeetingLink = eventModel.MeetingLink,
            StartDatetime = eventModel.StartDatetime
        };

        ICollection<EventParticipant> eventParticipants = eventModel.EventParticipants
            .Select(EventParticipantMapper.ToEntity)
            .ToList();
        
        foreach (EventParticipant eventParticipant in eventParticipants) entity.EventParticipants.Add(eventParticipant);
        return entity;
    }

    public static EventModel ToModel(Event eventEntity)
    {
        EventModel model = new EventModel
        {
            Id = eventEntity.Id,
            Name = eventEntity.Name,
            Description = eventEntity.Description,
            Status = (Models.Enums.EventStatus)eventEntity.Status,
            EndDatetime = eventEntity.EndDatetime,
            InviteCode = eventEntity.InviteCode,
            MaxParticipant = eventEntity.MaxParticipant,
            MeetingLink = eventEntity.MeetingLink,
            StartDatetime = eventEntity.StartDatetime
        };

        ICollection<EventParticipantModel> eventParticipantModels = eventEntity.EventParticipants
            .Select(EventParticipantMapper.ToModel)
            .ToList();
        
        model.EventParticipants = eventParticipantModels;
        return model;
    }
}