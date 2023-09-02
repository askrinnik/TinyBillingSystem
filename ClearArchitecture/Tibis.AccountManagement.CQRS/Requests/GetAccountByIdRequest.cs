using MediatR;
using Tibis.AccountManagement.CQRS.Models;

namespace Tibis.AccountManagement.CQRS.Requests;

public record GetAccountByIdRequest(Guid Id) : IRequest<AccountDto>;