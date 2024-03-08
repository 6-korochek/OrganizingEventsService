using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizingEventsService.Application.Contracts.Services;
using OrganizingEventsService.Application.Models.Dto.Account;

namespace OrganizingEventsService.Presentation.Http.Controllers;

[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("/register")]
    public ActionResult<AuthenticatedAccountDto> Register([FromBody] RegisterAccountDto registerAccountDto)
    {
        AuthenticatedAccountDto response = _authService.Register(registerAccountDto);
        return response;
    }
    
    [HttpPost("/login")]
    public ActionResult<AuthenticatedAccountDto> Login([FromBody] LoginAccountDto loginAccountDto)
    {
        AuthenticatedAccountDto response = _authService.Login(loginAccountDto);
        return response;
    }
    
    [Authorize("IsAuthenticated")]
    [HttpGet("/me")]
    public ActionResult<AccountDto> Authenticate(AuthenticatedAccountDto authenticatedAccountDto)
    {
        // Сразу получаем текущий аккаунт через миддлварь аутентификации (миддварь потом напишу)
        return authenticatedAccountDto.Account;
    }
}