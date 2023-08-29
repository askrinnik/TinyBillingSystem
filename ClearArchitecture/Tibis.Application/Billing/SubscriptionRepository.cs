using System.Collections.Concurrent;
using Tibis.Domain;
using Tibis.Domain.Billing;
using Tibis.Domain.Interfaces;

namespace Tibis.Application.Billing;

public class SubscriptionRepository:
    IRetrieveMany<Subscription>,
    ICreate<Subscription>,
    IRetrieve<Guid, Subscription>
{
    private readonly ConcurrentDictionary<Guid, Subscription> _items = new();

    public IAsyncEnumerable<Subscription> RetrieveManyAsync() => 
        _items.Values.ToAsyncEnumerable();

    public Task<Subscription> CreateAsync(Subscription item)
    {
        if (item.Id != Guid.Empty)
            throw new TibisValidationException("Id must be empty");

        if(_items.Values.Any(x => x.ProductId == item.AccountId))
            throw new SubscriptionAlreadyExistsException();

        var newItem = item with { Id = Guid.NewGuid() };
        _items.TryAdd(newItem.Id, newItem);
        return Task.FromResult(newItem);
    }

    public Task<Subscription?> TryRetrieveAsync(Guid key) => 
        Task.FromResult(_items.TryGetValue(key, out var item) ? item : null);
}