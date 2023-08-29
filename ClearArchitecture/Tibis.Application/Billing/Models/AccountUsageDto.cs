using Tibis.Domain.Billing;

namespace Tibis.Application.Billing.Models;

public record AccountUsageDto(Guid Id, Guid SubscriptionId, DateTime Date, int Amount)
{
    public static AccountUsageDto From(AccountUsage accountUsage) => 
        new(accountUsage.Id, accountUsage.SubscriptionId, accountUsage.Date, accountUsage.Amount);
}