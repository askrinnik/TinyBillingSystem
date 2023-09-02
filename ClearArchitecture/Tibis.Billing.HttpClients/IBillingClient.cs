using Tibis.Billing.CQRS.Models;
using Tibis.Billing.CQRS.Requests;

namespace Tibis.Billing.HttpClients;

internal interface IBillingClient
{
    Task<SubscriptionDto> GetSubscriptionAsync(Guid id, CancellationToken cancellationToken);
    Task<SubscriptionDto> GetSubscriptionAsync(Guid productId, Guid accountId, CancellationToken cancellationToken);
    Task<SubscriptionDto> CreateSubscriptionAsync(CreateSubscriptionRequest subscription, CancellationToken cancellationToken);
    Task<AccountUsageDto> MeterUsageAsync(MeterUsageRequest meterUsageRequest, CancellationToken cancellationToken);
    Task<IEnumerable<AccountUsageDto>> GetAllAccountUsagesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync(CancellationToken cancellationToken);
    Task<SubscriptionDto> GetSubscriptionByProductIdAccountIdAsync(GetSubscriptionByProductIdAccountIdRequest request, CancellationToken cancellationToken);
}