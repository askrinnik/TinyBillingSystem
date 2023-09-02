using System.Collections.Concurrent;
using Tibis.Billing.Domain;
using Tibis.Billing.Domain.Exceptions;
using Tibis.Contracts.Exceptions;
using Tibis.Contracts.Interfaces;

namespace Tibis.Billing.DB;

public class SubscriptionRepository:
    IRetrieveMany<Subscription>,
    ICreate<Subscription>,
    IRetrieve<Guid, Subscription>,
    IRetrieve<Guid, Guid, Subscription>
{
    private readonly ConcurrentDictionary<Guid, Subscription> _items = new();

    public IAsyncEnumerable<Subscription> RetrieveManyAsync() => 
        _items.Values.ToAsyncEnumerable();

    public Task<Subscription> CreateAsync(Subscription item)
    {
        if (item.Id != Guid.Empty)
            throw new TibisValidationException("Id must be empty");

        if(_items.Values.Any(x => x.ProductId == item.ProductId && x.AccountId == item.AccountId))
            throw new SubscriptionAlreadyExistsException();

        var newItem = item with { Id = Guid.NewGuid() };
        _items.TryAdd(newItem.Id, newItem);
        return Task.FromResult(newItem);
    }

    public Task<Subscription?> TryRetrieveAsync(Guid key) => 
        Task.FromResult(_items.TryGetValue(key, out var item) ? item : null);

    public Task<Subscription?> TryRetrieveAsync(Guid key1, Guid key2)
    {
        return Task.FromResult(_items.Values.SingleOrDefault(x => x.ProductId == key1 && x.AccountId == key2));
    }
}