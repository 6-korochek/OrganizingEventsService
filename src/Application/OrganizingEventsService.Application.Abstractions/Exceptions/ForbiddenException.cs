namespace OrganizingEventsService.Application.Abstractions.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException(string message = "Forbidden") : base(message) {}
}