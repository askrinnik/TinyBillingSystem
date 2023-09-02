using MediatR;
using Tibis.Billing.CQRS.Models;
using Tibis.Billing.CQRS.Requests;

namespace Tibis.Billing.HttpClients.Handlers;

internal class CreateSubscriptionHandler : IRequestHandler<CreateSubscriptionRequest, SubscriptionDto>
{
    private readonly IBillingClient _billingClient;

    public CreateSubscriptionHandler(IBillingClient billingClient) => _billingClient = billingClient;

    public async Task<SubscriptionDto> Handle(CreateSubscriptionRequest request, CancellationToken cancellationToken) => 
        await _billingClient.CreateSubscriptionAsync(request, cancellationToken);
}