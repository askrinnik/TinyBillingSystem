using MediatR;
using Tibis.Application.AccountManagement.Models;

namespace Tibis.Application.AccountManagement.Queries;

public record GetAccountByNameRequest(string Name) : IRequest<AccountDto>;