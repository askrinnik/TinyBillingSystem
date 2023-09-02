using MediatR;
using Tibis.AccountManagement.CQRS.Models;

namespace Tibis.AccountManagement.CQRS.Requests;

public record CreateAccountRequest(string Name) : IRequest<AccountDto>;