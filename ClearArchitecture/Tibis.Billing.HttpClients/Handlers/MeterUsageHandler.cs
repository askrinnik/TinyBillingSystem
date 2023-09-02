using MediatR;
using Tibis.Billing.CQRS.Models;
using Tibis.Billing.CQRS.Requests;

namespace Tibis.Billing.HttpClients.Handlers;

internal class MeterUsageHandler : IRequestHandler<MeterUsageRequest, AccountUsageDto>
{
    private readonly IBillingClient _billingClient;

    public MeterUsageHandler(IBillingClient billingClient) => _billingClient = billingClient;

    public async Task<AccountUsageDto> Handle(MeterUsageRequest request, CancellationToken cancellationToken) =>
        await _billingClient.MeterUsageAsync(request, cancellationToken);
}
