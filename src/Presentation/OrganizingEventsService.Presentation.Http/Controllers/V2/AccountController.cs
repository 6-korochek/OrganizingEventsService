using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizingEventsService.Application.CQRS.Commands;
using OrganizingEventsService.Application.CQRS.Queries;
using OrganizingEventsService.Application.Events.Commands;
using OrganizingEventsService.Application.Events.Queries;
using OrganizingEventsService.Application.Models.Dto.Account;

namespace OrganizingEventsService.Presentation.Http.Controllers.V2;

[Authorize("IsAuthenticated")]
[Route("/api/v2/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{accountId}")]
    [Authorize("IsAdmin")]
    public async Task<AccountDto> Get(Guid accountId)
    {
        AccountDto response = await _mediator.Send(new GetAccountByIdQuery() { AccountId = accountId });
        return response;
    }

    [HttpDelete("{accountId}")]
    [Authorize("IsAdmin")]
    public async Task Delete(Guid accountId)
    {
        await _mediator.Send(new DeleteAccountCommand() { AccountId = accountId });
    }

    [HttpPatch]
    public async Task<Guid> Update([FromBody] UpdateAccountDto updateAccountDto)
    {
        var currentAccount = HttpContext.Items["CurrentAccount"] as AuthenticatedAccountDto;
        Guid response = await _mediator.Send(new UpdateAccountCommand()
        {
            AccountId = currentAccount!.Account.Id,
            UpdateAccountDto = updateAccountDto
        });
        return response;
    }
}