using MediatR;
using Tibis.Application.AccountManagement.Models;

namespace Tibis.Application.AccountManagement.Queries;

public record CreateAccountRequest(string Name) : IRequest<AccountDto>;