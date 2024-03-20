using System.Collections.ObjectModel;

namespace OrganizingEventsService.Application.Abstractions.Persistence.Queries;

public class AccountQuery
{
    public Collection<Guid> Ids { get; } = new();

    public Collection<string> Emails { get; } = new();

    public ushort? Limit { get; private set; }

    public ushort? Offset { get; private set; }

    public AccountQuery WithId(Guid id)
    {
        Ids.Add(id);
        return this;
    }
    
    public AccountQuery WithId(IEnumerable<Guid> ids)
    {
        foreach (var id in ids)
        {
          WithId(id);
        }
        return this;
    }

    public AccountQuery WithEmail(string email)
    {
        Emails.Add(email);
        return this;
    }
    
    public AccountQuery WithEmail(IEnumerable<string> emails)
    {
        foreach (var email in emails)
        {
            WithEmail(email);
        }
        return this;
    }

    public AccountQuery WithLimit(ushort limit)
    {
        Limit = limit;
        return this;
    }

    public AccountQuery WithOffset(ushort offset)
    {
        Offset = offset;
        return this;
    }
}