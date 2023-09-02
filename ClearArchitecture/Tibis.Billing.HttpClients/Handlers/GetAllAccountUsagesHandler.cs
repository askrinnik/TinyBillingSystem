using MediatR;
using Tibis.Billing.CQRS.Models;
using Tibis.Billing.CQRS.Requests;

namespace Tibis.Billing.HttpClients.Handlers;

internal class GetAllAccountUsagesHandler : IRequestHandler<GetAllAccountUsagesRequest, IEnumerable<AccountUsageDto>>
{
    private readonly IBillingClient _billingClient;

    public GetAllAccountUsagesHandler(IBillingClient billingClient) => _billingClient = billingClient;

    public async Task<IEnumerable<AccountUsageDto>> Handle(GetAllAccountUsagesRequest request, CancellationToken cancellationToken) =>
        await _billingClient.GetAllAccountUsagesAsync(cancellationToken);
}