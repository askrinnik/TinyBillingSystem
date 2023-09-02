using MediatR;
using Tibis.AccountManagement.CQRS.Models;

namespace Tibis.AccountManagement.CQRS.Requests;

public record GetAccountByNameRequest(string Name) : IRequest<AccountDto>;