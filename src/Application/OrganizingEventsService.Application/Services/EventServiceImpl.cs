using OrganizingEventsService.Application.Abstractions.Persistence.Queries;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.ApplicationConstants;
using OrganizingEventsService.Application.Contracts.Services;
using OrganizingEventsService.Application.Exceptions;
using OrganizingEventsService.Application.Models.Dto.Account;
using OrganizingEventsService.Application.Models.Dto.Common;
using OrganizingEventsService.Application.Models.Dto.Event;
using OrganizingEventsService.Application.Models.Dto.Feedback;
using OrganizingEventsService.Application.Models.Dto.Participant;
using OrganizingEventsService.Application.Models.Dto.Role;
using OrganizingEventsService.Application.Models.Entities;
using OrganizingEventsService.Application.Models.Entities.Enums;
using System.Security.Cryptography;
using System.Text;

namespace OrganizingEventsService.Application.Services;

public class EventServiceImpl : EventService
{
    private readonly IEventRepository _eventRepository;
    
    public EventServiceImpl(IAccountService accountService, IEventRepository eventRepository) : base(accountService)
    {
        _eventRepository = eventRepository;
    }

    public override async Task<EventDto> GetEventInfo(Guid? eventId, string? inviteCode)
    {
        Event eventEntity = null!;
        if (inviteCode != null)
        {
            eventEntity = await _eventRepository.GetEventByInviteCode(inviteCode);
        } else if (eventId != null)
        {
            eventEntity = await _eventRepository.GetById((Guid)eventId);
        }

        var query = new EventParticipantQuery()
            .WithEventId(eventEntity.Id)
            .WithRoleId(Roles.ORGANIZER)
            .WithAccount()
            .WithLimit(1);

        var organizer = await _eventRepository.GetParticipantListByQuery(query).FirstAsync();
        return new EventDto // Это п*зд*ц заменим на AutoMapper
        {
            Id = eventEntity.Id,
            Name = eventEntity.Name,
            Description = eventEntity.Description,
            MeetingLink = eventEntity.MeetingLink,
            InviteCode = eventEntity.InviteCode,
            Status = eventEntity.Status,
            MaxParticipant = eventEntity.MaxParticipant,
            StartDatetime = eventEntity.StartDatetime,
            EndDatetime = eventEntity.EndDatetime,
            Organizer = new OrganizerDto
            {
                AccountId = organizer!.AccountIdNavigation!.Id,
                Name = organizer.AccountIdNavigation.Name,
                Surname = organizer.AccountIdNavigation.Surname
            }
        };
    }

    public override IAsyncEnumerable<EventDto> GetEventsWhereAccountIsParticipant(Guid accountId, EventStatus status, PaginationDto? paginationDto)
    {
        var query = new EventQuery()
            .WithStatus(status)
            .WithAccountParticipantId(accountId);

        if (paginationDto != null)
        {
            query
                .WithLimit(paginationDto.Limit)
                .WithOffset(paginationDto.Offset);
        }

        var events = _eventRepository
            .GetListByQuery(query)
            .Select(eventEntity => new EventDto // Это п*зд*ц заменим на AutoMapper
            {
                Id = eventEntity.Id,
                Name = eventEntity.Name,
                Description = eventEntity.Description,
                MeetingLink = eventEntity.MeetingLink,
                InviteCode = eventEntity.InviteCode,
                Status = eventEntity.Status,
                MaxParticipant = eventEntity.MaxParticipant,
                StartDatetime = eventEntity.StartDatetime,
                EndDatetime = eventEntity.EndDatetime
            });

        return events;

    }

    public override async Task<NewEventDto> CreateEvent(Guid organizerId, CreateEventDto createEventDto)
    {
        var eventEntity = new Event
        {
            Id = new Guid(),
            Name = createEventDto.Name,
            Description = createEventDto.Description,
            MeetingLink = createEventDto.MeetingLink,
            StartDatetime = createEventDto.StartDate,
            Status = createEventDto.EventStatus,
            EndDatetime = createEventDto.EndDate,
            MaxParticipant = createEventDto.MaxParticipant,
            InviteCode = GenerateInviteCode()
        };

        var organizer = new EventParticipant
        {
            Id = new Guid(),
            AccountId = organizerId,
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

    public override async Task<Guid> PartiallyUpdateEvent(Guid eventId, UpdateEventDto updateEventDto)
    {
        var eventEntity = await _eventRepository.GetById(eventId);
        if (eventEntity.Status is EventStatus.IsOver)
        {
            throw new Exception("Event is over!"); // Потом кастомные добавим
        }
        
        eventEntity.MeetingLink = updateEventDto.MeetingLink ?? eventEntity.MeetingLink;
        if (eventEntity.Status is not EventStatus.InGoing)
        {
            eventEntity.Name = updateEventDto.Name ?? eventEntity.Name;
            eventEntity.Description = updateEventDto.Description ?? eventEntity.Description;
            eventEntity.MaxParticipant = updateEventDto.MaxParticipant ?? eventEntity.MaxParticipant;
            eventEntity.StartDatetime = updateEventDto.StartDate ?? eventEntity.StartDatetime;
            eventEntity.EndDatetime = updateEventDto.EndDate ?? eventEntity.EndDatetime;
            eventEntity.Status = updateEventDto.EventStatus ?? eventEntity.Status;
        
            await _eventRepository.Update(eventEntity);
            return eventEntity.Id;
        }

        if (updateEventDto.EventStatus is EventStatus.IsOver)
        {
            eventEntity.Status = EventStatus.IsOver;
        }
        
        await _eventRepository.Update(eventEntity);
        return eventEntity.Id;
    }

    public override async Task DeleteEventById(Guid eventId)
    {
        await _eventRepository.DeleteById(eventId);
    }

    public override IAsyncEnumerable<ParticipantDto> GetParticipants(Guid eventId)
    {
        var eventEntity = _eventRepository.GetById(eventId);
        var query = new EventParticipantQuery().WithEventId(eventEntity.Result.Id).WithAccount().WithRole();
        var eventParticipantEntities = _eventRepository.GetParticipantListByQuery(query);
        var participants = from e in eventParticipantEntities
            select new ParticipantDto()
            {
                AccountId = e.AccountId,
                AccountName = e!.AccountIdNavigation!.Name,
                AccountSurName = e!.AccountIdNavigation!.Surname,
                Role = new RoleDto()
                {
                    Id = e.RoleId,
                    Name = e!.RoleIdNavigation!.Name
                },
                Status = e.InviteStatus
            };
        
        return participants;
    }

    public override async Task<ParticipantDto> GetParticipantInEvent(Guid eventId, Guid accountId)
    {
        var participantEntity = await _eventRepository.GetParticipantInEvent(accountId, eventId, true, true);
        var participantDto = new ParticipantDto()
        {
            AccountId = participantEntity.AccountId,
            AccountName = participantEntity!.AccountIdNavigation!.Name,
            AccountSurName = participantEntity.AccountIdNavigation.Surname,
            Role = new RoleDto()
            {
                Id = participantEntity.RoleId,
                Name = participantEntity!.RoleIdNavigation!.Name
            }
        };
        
        return participantDto;
    }

    public override async void CreateParticipants(
        Guid eventId,
        IEnumerable<CreateParticipantDto> createParticipantDtoList)
    {
        var eventEntity = await _eventRepository.GetById(eventId);

        IEnumerable<CreateParticipantDto> createParticipantDtos = createParticipantDtoList as CreateParticipantDto[] ?? createParticipantDtoList.ToArray();
        var accountEmails = from dto in createParticipantDtos select dto.AccountEmail;
        var existingAccountDtoList = AccountService.GetExistingAccountsByEmail(accountEmails);

        IEnumerable<AccountDto> accountDtoList = existingAccountDtoList as AccountDto[] ?? existingAccountDtoList.ToArray();
        if (!accountDtoList.Any())
        {
            return;
        }

        var newEventParticipants = from createParticipantDto in createParticipantDtos
            where accountDtoList.Select(acc => acc.Email)
                .Contains(createParticipantDto.AccountEmail)
            let account = accountDtoList.First(c =>
                c.Email == createParticipantDto.AccountEmail)
            select new EventParticipant()
            {
                Id = new Guid(),
                EventId = eventId,
                AccountId = account.Id,
                InviteStatus = EventParticipantInviteStatus.Pending,
                RoleId = createParticipantDto.RoleId
            };
        
        await _eventRepository.AddParticipants(eventEntity.Id, newEventParticipants);
    }

    public override async Task<ParticipantDto> PartiallyUpdateParticipant(
        Guid eventId,
        Guid accountId,
        UpdateParticipantDto updateParticipantDto)
    {
        var participantEntity = await _eventRepository.GetParticipantInEvent(accountId, eventId, true, true);

        if (participantEntity.IsBanned)
        {
            throw new ConflictException("Cannot update banned user.");
        }
        
        if (participantEntity.InviteStatus == EventParticipantInviteStatus.Pending)
        {
            participantEntity.InviteStatus = updateParticipantDto.InviteStatus ?? participantEntity.InviteStatus;
        }

        participantEntity.IsBanned = updateParticipantDto.IsBanned ?? participantEntity.IsBanned;
        participantEntity.RoleId = updateParticipantDto.RoleId ?? participantEntity.RoleId;
        
        await _eventRepository.UpdateParticipant(participantEntity);

        return new ParticipantDto()
        {
            AccountId = participantEntity.AccountId,
            AccountName = participantEntity!.AccountIdNavigation!.Name,
            AccountSurName = participantEntity.AccountIdNavigation.Surname,
            Role = new RoleDto() { Id = participantEntity.RoleId, Name = participantEntity!.RoleIdNavigation!.Name },
            Status = participantEntity.InviteStatus
        };
    }

    public override void DeleteParticipantsByEmails(IEnumerable<string> accountEmails)
    {
        throw new NotImplementedException();
    }

    public override void DeleteParticipantByAccountId(Guid accountId)
    {
        throw new NotImplementedException();
    }

    public override FeedbackDto GetFeedbackInfo(Guid feedbackId)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<FeedbackDto> GetFeedbacksByEventId(Guid eventId)
    {
        throw new NotImplementedException();
    }

    public override Guid CreateFeedback(Guid eventId, CreateFeedbackDto createFeedbackDto)
    {
        throw new NotImplementedException();
    }

    public override FeedbackDto PartiallyUpdateFeedback(Guid feedbackId, UpdateFeedbackDto updateFeedbackDto)
    {
        throw new NotImplementedException();
    }

    public override void DeleteFeedbackById(Guid feedbackId)
    {
        throw new NotImplementedException();
    }

    private string GenerateInviteCode(ushort lenght = 10)
    {
        StringBuilder stringBuilder = new StringBuilder();
        using (var hash = SHA256.Create())
        {
            byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(new Guid().ToString()));
            foreach (byte b in result)
                stringBuilder.Append(b.ToString("x2"));
        }

        var inviteCode = stringBuilder.ToString();
        return inviteCode.Length > lenght ? inviteCode[..lenght] : inviteCode;
    }
}