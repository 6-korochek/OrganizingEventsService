using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizingEventsService.Application.Contracts.Services;
using OrganizingEventsService.Application.Models.Dto.Account;

namespace OrganizingEventsService.Presentation.Http.Controllers.V1;

[Authorize("IsAuthenticated")]
[Authorize("IsAdmin")]
[Route("[controller]/{accountId}")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpGet]
    public async Task<AccountDto> Get(Guid accountId)
    {
        AccountDto account = await _accountService.GetAccountById(accountId);
        return account;
    }
    
    [HttpDelete]
    public void Delete(Guid accountId)
    {
        _accountService.DeleteAccountById(accountId);
    }
}