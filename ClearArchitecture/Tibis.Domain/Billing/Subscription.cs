using Tibis.Domain.ProductManagement;

namespace Tibis.Domain.Billing;

public record Subscription(Guid Id, Guid ProductId, Guid AccountId)
{
    /// <summary>
    /// Create a new instance of <see cref="Product"/>/>.
    /// </summary>
    public Subscription(Guid productId, Guid accountId) : this(Guid.Empty, productId, accountId)
    { }
}