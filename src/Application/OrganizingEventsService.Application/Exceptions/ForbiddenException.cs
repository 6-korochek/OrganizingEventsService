namespace OrganizingEventsService.Application.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException(string message = "Forbidden") : base(message) {}
}