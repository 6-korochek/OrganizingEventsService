using System.Collections.ObjectModel;

namespace OrganizingEventsService.Application.Abstractions.Persistence.Queries;

public class FeedbackQuery
{
    public Collection<Guid> EventIds { get; } = new();
    
    public bool IncludeAuthor { get; private set; }

    public FeedbackQuery WithAuthor(bool includeAuthor = true)
    {
        IncludeAuthor = includeAuthor;
        return this;
    }

    public FeedbackQuery WithEventId(Guid eventId)
    {
        EventIds.Add(eventId);
        return this;
    }
}