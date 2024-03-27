using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizingEventsService.Application.Auth.Commands;
using OrganizingEventsService.Application.Models.Dto.Account;

namespace OrganizingEventsService.Presentation.Http.Controllers.V2;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("/register")]
    public async Task<AuthenticatedAccountDto> Register([FromBody] RegisterAccountDto registerAccountDto)
    {
        AuthenticatedAccountDto response = await _mediator.Send(new RegisterCommand { Name = registerAccountDto.Name, Surname = registerAccountDto.Surname, Email = registerAccountDto.Email, Password = registerAccountDto.Password});
        return response;
    }
    
    [HttpPost("/login")]
    public async Task<AuthenticatedAccountDto> Login([FromBody] LoginAccountDto loginAccountDto)
    {
        AuthenticatedAccountDto response = await _mediator.Send(new LoginCommand {Email = loginAccountDto.Email, Password = loginAccountDto.Password});
        return response;
    }
    
    [Authorize("IsAuthenticated")]
    [HttpGet("/me")]
    public ActionResult<AccountDto> Authenticate()
    {
        var currentAccount = HttpContext.Items["CurrentAccount"] as AuthenticatedAccountDto;
        return currentAccount!.Account;
    }
}