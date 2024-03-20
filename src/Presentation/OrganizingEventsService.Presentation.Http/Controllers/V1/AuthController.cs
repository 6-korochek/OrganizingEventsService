using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizingEventsService.Application.Contracts.Services;
using OrganizingEventsService.Application.Models.Dto.Account;

namespace OrganizingEventsService.Presentation.Http.Controllers.V1;

[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("/register")]
    public async Task<AuthenticatedAccountDto> Register([FromBody] RegisterAccountDto registerAccountDto)
    {
        AuthenticatedAccountDto response = await _authService.Register(registerAccountDto);
        return response;
    }
    
    [HttpPost("/login")]
    public async Task<AuthenticatedAccountDto> Login([FromBody] LoginAccountDto loginAccountDto)
    {
        AuthenticatedAccountDto response = await _authService.Login(loginAccountDto);
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