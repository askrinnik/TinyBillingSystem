using Tibis.Domain;

namespace Tibis.Application.Billing;

public class SubscriptionAlreadyExistsException : ItemAlreadyExistsException
{
    public SubscriptionAlreadyExistsException() : base($"Subscription already exists.")
    {
    }
}

public class SubscriptionNotFoundException : ItemNotFoundException
{
    public SubscriptionNotFoundException(Guid id) : base($"Subscription with id {id} was not found.")
    {
    }
    public SubscriptionNotFoundException(Guid productId, Guid accountId) : base($"Subscription with product id {productId} and account id {accountId} was not found.")
    {
    }
}