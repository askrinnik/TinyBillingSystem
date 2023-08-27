using MediatR;
using Tibis.Application.AccountManagement.Models;
using Tibis.Application.AccountManagement.Queries;
using Tibis.Domain.AccountManagement;
using Tibis.Domain.Interfaces;

namespace Tibis.Application.AccountManagement.Handlers;

public class GetAllAccountsHandler : IRequestHandler<GetAllAccountsRequest, IEnumerable<AccountDto>>
{
    private readonly IRetrieveMany<Account> _accountRepository;

    public GetAllAccountsHandler(IRetrieveMany<Account> accountRepository) => 
        _accountRepository = accountRepository;

    public async Task<IEnumerable<AccountDto>> Handle(GetAllAccountsRequest request, CancellationToken cancellationToken) =>
        await _accountRepository.RetrieveManyAsync()
            .SelectAwait(item => ValueTask.FromResult(AccountDto.FromAccount(item)))
            .ToArrayAsync(cancellationToken);
}