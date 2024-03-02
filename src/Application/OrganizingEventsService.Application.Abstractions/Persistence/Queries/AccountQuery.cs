using SourceKit.Generators.Builder.Annotations;

namespace OrganizingEventsService.Application.Abstractions.Persistence.Queries;

[GenerateBuilder]
public partial record AccountQuery(
    IReadOnlyCollection<Guid> Ids,
    IReadOnlyCollection<string> Emails,
    ushort? Limit,
    ushort? Offset);