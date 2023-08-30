using Tibis.Application.Billing.Queries;
using Tibis.Domain.Billing;

namespace Tibis.Application.Billing.Services;

public interface IBillingClient
{
    Task<Subscription> GetSubscriptionAsync(Guid id);
    Task<Subscription> GetSubscriptionAsync(Guid productId, Guid accountId);
    Task<Subscription> CreateSubscriptionAsync(Subscription subscription);
    Task<AccountUsage> MeterUsageAsync(MeterUsageRequest meterUsageRequest);
}