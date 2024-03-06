using Microsoft.AspNetCore.Mvc;
using OrganizingEventsService.Application.Contracts.Services;
using OrganizingEventsService.Application.Models.Dto.Account;

namespace OrganizingEventsService.Presentation.Http.Controllers;

[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    // Only Admin
    [HttpGet("{id}")]
    public ActionResult<AccountDto> Get(Guid id)
    {
        AccountDto account = _accountService.GetAccountById(id);
        return account;
    }
    
    // Only Admin
    [HttpDelete("{id}")]
    public void Delete(Guid id)
    {
        _accountService.DeleteAccountById(id);
    }
}