namespace Tibis.Billing.CQRS.Models;

public record SubscriptionDto(Guid Id, Guid ProductId, Guid AccountId);