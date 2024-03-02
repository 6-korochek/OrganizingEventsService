using SourceKit.Generators.Builder.Annotations;

namespace OrganizingEventsService.Application.Abstractions.Persistence.Queries;

[GenerateBuilder]
public partial record EventParticipantQuery(
    Guid AccountId,
    Guid EventId,
    ushort? Limit,
    ushort? Offset);