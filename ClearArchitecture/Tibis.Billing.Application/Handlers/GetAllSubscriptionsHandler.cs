using MediatR;
using Tibis.Billing.CQRS.Models;
using Tibis.Billing.CQRS.Requests;
using Tibis.Billing.Domain;
using Tibis.Contracts.Interfaces;

namespace Tibis.Billing.Application.Handlers;

internal class GetAllSubscriptionsHandler : IRequestHandler<GetAllSubscriptionsRequest, IEnumerable<SubscriptionDto>>
{
    private readonly IRetrieveMany<Subscription> _repository;

    public GetAllSubscriptionsHandler(IRetrieveMany<Subscription> repository) => 
        _repository = repository;

    public async Task<IEnumerable<SubscriptionDto>> Handle(GetAllSubscriptionsRequest request, CancellationToken cancellationToken) =>
        await _repository.RetrieveManyAsync()
            .SelectAwait(item => ValueTask.FromResult(item.ToDto()))
            .ToArrayAsync(cancellationToken);
}