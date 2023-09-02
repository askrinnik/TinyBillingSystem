using Tibis.Billing.CQRS.Models;
using Tibis.Billing.Domain;

namespace Tibis.Billing.Application;

internal static class DtoExtensions
{
    public static AccountUsageDto ToDto(this AccountUsage accountUsage) =>
        new(accountUsage.Id, accountUsage.SubscriptionId, accountUsage.Date, accountUsage.Amount);

    public static SubscriptionDto ToDto(this Subscription subscription) => new(subscription.Id, subscription.ProductId, subscription.AccountId);
}