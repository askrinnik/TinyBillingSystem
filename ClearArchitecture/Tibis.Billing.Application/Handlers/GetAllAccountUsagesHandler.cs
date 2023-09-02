using MediatR;
using Tibis.Billing.CQRS.Models;
using Tibis.Billing.CQRS.Requests;
using Tibis.Billing.Domain;
using Tibis.Contracts.Interfaces;

namespace Tibis.Billing.Application.Handlers;

internal class GetAllAccountUsagesHandler : IRequestHandler<GetAllAccountUsagesRequest, IEnumerable<AccountUsageDto>>
{
    private readonly IRetrieveMany<AccountUsage> _repository;

    public GetAllAccountUsagesHandler(IRetrieveMany<AccountUsage> repository) => 
        _repository = repository;

    public async Task<IEnumerable<AccountUsageDto>> Handle(GetAllAccountUsagesRequest request, CancellationToken cancellationToken) =>
        await _repository.RetrieveManyAsync()
            .SelectAwait(item => ValueTask.FromResult(item.ToDto()))
            .ToArrayAsync(cancellationToken);
}