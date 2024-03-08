namespace OrganizingEventsService.Presentation.Http.Requirements.IsAuthenticated;

public class AuthenticationRequirement : BaseRequirement
{
    public string AuthenticationHeaderName { get; }

    public AuthenticationRequirement(string authenticationHeaderName = "Authorization") : base("IsAuthenticated")
    {
        AuthenticationHeaderName = authenticationHeaderName;
    }
}