using OrganizingEventsService.Application.Models.Entities.Enums;
using SourceKit.Generators.Builder.Annotations;

namespace OrganizingEventsService.Application.Abstractions.Persistence.Queries;

[GenerateBuilder]
public partial record EventQuery(
    IReadOnlyCollection<Guid> Ids,
    IReadOnlyCollection<EventStatus> Statuses,
    ushort? Limit,
    ushort? Offset,
    bool IncludeParticipants = false);