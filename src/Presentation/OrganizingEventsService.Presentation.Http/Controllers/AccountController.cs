using Microsoft.AspNetCore.Mvc;
using OrganizingEventsService.Application.Abstractions.Persistence.Repositories;
using OrganizingEventsService.Application.Models.Entities;
using OrganizingEventsService.Application.Abstractions.Persistence.Queries;

namespace OrganizingEventsService.Presentation.Http.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;

    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    [HttpGet("{id}")]
    public IAsyncEnumerable<Account> Get(Guid id)
    {
        AccountQuery query = new AccountQuery().WithId(id).WithEmail("Фиг вам");
        IAsyncEnumerable<Account> accounts = _accountRepository.GetListByQuery(query);
        return accounts;
    }

    [HttpPost("")]
    public async Task<ActionResult<Account>> Create()
    {
        var account = new Account
        {
            Id = new Guid(),
            Name = "Andrey",
            Surname = "Kotovschikow",
            Email = "Фиг вам",
            PasswordHash = "jjnfslnnfskfsfs",
            IsInvite = true,
            IsAdmin = true,
            CreatedAt = new DateTime(),
            PasswordHashUpdatedAt = new DateTime()
        };
        
        await _accountRepository.Add(account);
        return account;
    }
}