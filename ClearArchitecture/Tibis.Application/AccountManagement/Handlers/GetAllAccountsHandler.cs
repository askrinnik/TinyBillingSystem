using MediatR;
using Tibis.Application.AccountManagement.Models;
using Tibis.Application.AccountManagement.Queries;
using Tibis.Domain.AccountManagement;
using Tibis.Domain.Interfaces;

namespace Tibis.Application.AccountManagement.Handlers;

public class GetAllAccountsHandler : IRequestHandler<GetAllAccountsRequest, IEnumerable<AccountDto>>
{
    private readonly IRetrieveMany<Account> _repository;

    public GetAllAccountsHandler(IRetrieveMany<Account> repository) => 
        _repository = repository;

    public async Task<IEnumerable<AccountDto>> Handle(GetAllAccountsRequest request, CancellationToken cancellationToken) =>
        await _repository.RetrieveManyAsync()
            .SelectAwait(item => ValueTask.FromResult(AccountDto.From(item)))
            .ToArrayAsync(cancellationToken);
}