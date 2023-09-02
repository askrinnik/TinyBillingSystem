using MediatR;
using Tibis.Billing.CQRS.Models;
using Tibis.Billing.CQRS.Requests;

namespace Tibis.Billing.HttpClients.Handlers;

internal class GetSubscriptionByIdHandler: IRequestHandler<GetSubscriptionByIdRequest, SubscriptionDto>
{
    private readonly IBillingClient _billingClient;

    public GetSubscriptionByIdHandler(IBillingClient billingClient) => _billingClient = billingClient;

    public async Task<SubscriptionDto> Handle(GetSubscriptionByIdRequest request, CancellationToken cancellationToken) =>
        await _billingClient.GetSubscriptionAsync(request.Id, cancellationToken);
}