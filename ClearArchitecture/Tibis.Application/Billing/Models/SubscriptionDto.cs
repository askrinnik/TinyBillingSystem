using Tibis.Domain.Billing;

namespace Tibis.Application.Billing.Models;

public record SubscriptionDto(Guid Id, Guid ProductId, Guid AccountId)
{
    public static SubscriptionDto From(Subscription subscription) => new(subscription.Id, subscription.ProductId, subscription.AccountId);
}