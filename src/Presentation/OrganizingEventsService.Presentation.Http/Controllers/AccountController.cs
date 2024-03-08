using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizingEventsService.Application.Contracts.Services;
using OrganizingEventsService.Application.Models.Dto.Account;

namespace OrganizingEventsService.Presentation.Http.Controllers;

[Authorize("IsAuthenticated")]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [Authorize("IsAdmin")]
    [HttpGet("{accountId}")]
    public ActionResult<AccountDto> Get(Guid accountId)
    {
        AccountDto account = _accountService.GetAccountById(accountId);
        return account;
    }
    
    [Authorize("IsAdmin")]
    [HttpDelete("{accountId}")]
    public void Delete(Guid accountId)
    {
        _accountService.DeleteAccountById(accountId);
    }
}