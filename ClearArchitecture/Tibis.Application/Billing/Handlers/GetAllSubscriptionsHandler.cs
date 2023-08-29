using MediatR;
using Tibis.Application.Billing.Models;
using Tibis.Application.Billing.Queries;
using Tibis.Domain.Billing;
using Tibis.Domain.Interfaces;

namespace Tibis.Application.Billing.Handlers;

public class GetAllSubscriptionsHandler : IRequestHandler<GetAllSubscriptionsRequest, IEnumerable<SubscriptionDto>>
{
    private readonly IRetrieveMany<Subscription> _repository;

    public GetAllSubscriptionsHandler(IRetrieveMany<Subscription> repository) => 
        _repository = repository;

    public async Task<IEnumerable<SubscriptionDto>> Handle(GetAllSubscriptionsRequest request, CancellationToken cancellationToken) =>
        await _repository.RetrieveManyAsync()
            .SelectAwait(item => ValueTask.FromResult(SubscriptionDto.From(item)))
            .ToArrayAsync(cancellationToken);
}