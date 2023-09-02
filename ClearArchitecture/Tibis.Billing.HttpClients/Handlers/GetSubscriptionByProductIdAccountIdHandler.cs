using MediatR;
using Tibis.Billing.CQRS.Models;
using Tibis.Billing.CQRS.Requests;

namespace Tibis.Billing.HttpClients.Handlers;

internal class GetSubscriptionByProductIdAccountIdHandler : IRequestHandler<GetSubscriptionByProductIdAccountIdRequest, SubscriptionDto>
{
    private readonly IBillingClient _billingClient;

    public GetSubscriptionByProductIdAccountIdHandler(IBillingClient billingClient) => _billingClient = billingClient;

    public async Task<SubscriptionDto> Handle(GetSubscriptionByProductIdAccountIdRequest request, CancellationToken cancellationToken) =>
       await _billingClient.GetSubscriptionByProductIdAccountIdAsync(request, cancellationToken);
}