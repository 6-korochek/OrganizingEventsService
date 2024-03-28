using MediatR;
using OrganizingEventsService.Application.Models.Dto.Account;

namespace OrganizingEventsService.Application.Events.Queries;

public class GetAccountByIdQuery: IRequest<AccountDto>
{
    public Guid AccountId { get; set; }
}