using Tibis.Contracts.Exceptions;

namespace Tibis.Billing.Domain.Exceptions;

public class SubscriptionAlreadyExistsException : ItemAlreadyExistsException
{
    public SubscriptionAlreadyExistsException() : base($"Subscription already exists.")
    {
    }
}