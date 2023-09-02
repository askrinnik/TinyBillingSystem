using MediatR;
using Tibis.AccountManagement.CQRS.Models;

namespace Tibis.AccountManagement.CQRS.Requests;

public record GetAllAccountsRequest : IRequest<IEnumerable<AccountDto>>;