using MediatR;
using Tibis.Application.Billing.Models;
using Tibis.Application.Billing.Queries;
using Tibis.Domain.Billing;
using Tibis.Domain.Interfaces;

namespace Tibis.Application.Billing.Handlers;

public class GetAllAccountUsagesHandler : IRequestHandler<GetAllAccountUsagesRequest, IEnumerable<AccountUsageDto>>
{
    private readonly IRetrieveMany<AccountUsage> _repository;

    public GetAllAccountUsagesHandler(IRetrieveMany<AccountUsage> repository) => 
        _repository = repository;

    public async Task<IEnumerable<AccountUsageDto>> Handle(GetAllAccountUsagesRequest request, CancellationToken cancellationToken) =>
        await _repository.RetrieveManyAsync()
            .SelectAwait(item => ValueTask.FromResult(AccountUsageDto.From(item)))
            .ToArrayAsync(cancellationToken);
}