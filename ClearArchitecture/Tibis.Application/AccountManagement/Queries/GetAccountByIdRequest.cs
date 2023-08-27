using MediatR;
using Tibis.Application.AccountManagement.Models;

namespace Tibis.Application.AccountManagement.Queries;

public record GetAccountByIdRequest(Guid Id) : IRequest<AccountDto>;