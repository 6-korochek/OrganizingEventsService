namespace OrganizingEventsService.Application.Models.Dto.Common;

public class PaginationDto
{
    public ushort Limit { get; set; }

    public ushort Offset { get; set; }
}