using MediatR;
using Tibis.Billing.CQRS.Models;
using Tibis.Billing.CQRS.Requests;

namespace Tibis.Billing.HttpClients.Handlers;

internal class GetAllSubscriptionsHandler : IRequestHandler<GetAllSubscriptionsRequest, IEnumerable<SubscriptionDto>>
{
    private readonly IBillingClient _billingClient;

    public GetAllSubscriptionsHandler(IBillingClient billingClient) => _billingClient = billingClient;

    public async Task<IEnumerable<SubscriptionDto>> Handle(GetAllSubscriptionsRequest request, CancellationToken cancellationToken) =>
        await _billingClient.GetAllSubscriptionsAsync(cancellationToken);
}