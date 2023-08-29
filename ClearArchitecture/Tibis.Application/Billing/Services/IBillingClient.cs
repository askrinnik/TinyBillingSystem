using Tibis.Domain.Billing;

namespace Tibis.Application.Billing.Services;

public interface IBillingClient
{
    Task<Subscription> GetSubscriptionAsync(Guid id);
    Task<Subscription> CreateSubscriptionAsync(Subscription subscription);
}