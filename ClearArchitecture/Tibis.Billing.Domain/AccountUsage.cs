namespace Tibis.Billing.Domain;

public record AccountUsage(Guid Id, Guid SubscriptionId, DateTime Date, int Amount)
{
    public AccountUsage(Guid subscriptionId, DateTime date, int amount) : this(Guid.Empty, subscriptionId, date, amount)
    { }
}