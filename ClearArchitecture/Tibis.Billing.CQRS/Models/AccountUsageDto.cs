namespace Tibis.Billing.CQRS.Models;

public record AccountUsageDto(Guid Id, Guid SubscriptionId, DateTime Date, int Amount);