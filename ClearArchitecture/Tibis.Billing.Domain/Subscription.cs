namespace Tibis.Billing.Domain;

public record Subscription(Guid Id, Guid ProductId, Guid AccountId)
{
    /// <summary>
    /// Create a new instance of <see cref="Subscription"/>/>.
    /// </summary>
    public Subscription(Guid productId, Guid accountId) : this(Guid.Empty, productId, accountId)
    { }
}