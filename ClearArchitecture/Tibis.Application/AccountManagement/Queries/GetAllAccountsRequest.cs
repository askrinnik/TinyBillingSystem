using MediatR;
using Tibis.Application.AccountManagement.Models;

namespace Tibis.Application.AccountManagement.Queries;

public record GetAllAccountsRequest : IRequest<IEnumerable<AccountDto>>;