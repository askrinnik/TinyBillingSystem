using MediatR;
using Tibis.AccountManagement.CQRS.Models;
using Tibis.AccountManagement.CQRS.Requests;
using Tibis.AccountManagement.Domain;
using Tibis.Contracts.Interfaces;

namespace Tibis.AccountManagement.Application.Handlers;

internal class GetAllAccountsHandler : IRequestHandler<GetAllAccountsRequest, IEnumerable<AccountDto>>
{
    private readonly IRetrieveMany<Account> _repository;

    public GetAllAccountsHandler(IRetrieveMany<Account> repository) => 
        _repository = repository;

    public async Task<IEnumerable<AccountDto>> Handle(GetAllAccountsRequest request, CancellationToken cancellationToken) =>
        await _repository.RetrieveManyAsync()
            .SelectAwait(item => ValueTask.FromResult(item.ToDto()))
            .ToArrayAsync(cancellationToken);
}