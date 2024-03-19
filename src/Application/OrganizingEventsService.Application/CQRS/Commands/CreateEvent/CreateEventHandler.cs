using MediatR;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.ApplicationConstants;
using OrganizingEventsService.Application.Models.Dto.Event;
using OrganizingEventsService.Application.Models.Entities;
using OrganizingEventsService.Application.Models.Entities.Enums;
using System.Security.Cryptography;
using System.Text;

namespace OrganizingEventsService.Application.CQRS.Commands.CreateEvent;

public class CreateEventHandler : IRequestHandler<CreateEventCommand, NewEventDto>
{
    private readonly IEventRepository _eventRepository;

    public CreateEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<NewEventDto> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = new Event
        {
            Id = Guid.NewGuid(),
            Name = request.EventRequest.Name,
            Description = request.EventRequest.Description,
            MeetingLink = request.EventRequest.MeetingLink,
            StartDatetime = request.EventRequest.StartDate,
            Status = request.EventRequest.EventStatus,
            EndDatetime = request.EventRequest.EndDate,
            MaxParticipant = request.EventRequest.MaxParticipant,
            InviteCode = GenerateInviteCode()
        };

        var organizer = new EventParticipant
        {
            Id = Guid.NewGuid(),
            AccountId = request.OrganizerId,
            EventId = eventEntity.Id,
            InviteStatus = EventParticipantInviteStatus.Accepted,
            RoleId = Roles.ORGANIZER
        };
        
        eventEntity.EventParticipants.Add(organizer);
        await _eventRepository.Add(eventEntity);
        
        return new NewEventDto
        {
            Id = eventEntity.Id,
            InviteCode = eventEntity.InviteCode
        };
    }
    
    private string GenerateInviteCode(ushort lenght = 10)
    {
        StringBuilder stringBuilder = new StringBuilder();
        using (var hash = SHA256.Create())
        {
            byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
            foreach (byte b in result)
                stringBuilder.Append(b.ToString("x2"));
        }

        var inviteCode = stringBuilder.ToString();
        return inviteCode.Length > lenght ? inviteCode[..lenght] : inviteCode;
    }
}