namespace OrganizingEventsService.Application.Contracts.Common;

public class PaginationDto
{
    public uint Limit { get; set; }
    
    public uint Offset { get; set; }
}