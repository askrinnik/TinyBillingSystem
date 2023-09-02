using System.Collections.Concurrent;
using Tibis.Billing.Domain;
using Tibis.Contracts.Exceptions;
using Tibis.Contracts.Interfaces;

namespace Tibis.Billing.DB;

internal class AccountUsageRepository:
    IRetrieveMany<AccountUsage>,
    ICreate<AccountUsage>,
    IRetrieve<Guid, AccountUsage>
{
    private readonly ConcurrentDictionary<Guid, AccountUsage> _items = new();

    public IAsyncEnumerable<AccountUsage> RetrieveManyAsync() => 
        _items.Values.ToAsyncEnumerable();

    public Task<AccountUsage> CreateAsync(AccountUsage item)
    {
        if (item.Id != Guid.Empty)
            throw new TibisValidationException("Id must be empty");

        var newItem = item with { Id = Guid.NewGuid() };
        _items.TryAdd(newItem.Id, newItem);
        return Task.FromResult(newItem);
    }

    public Task<AccountUsage?> TryRetrieveAsync(Guid key) => 
        Task.FromResult(_items.TryGetValue(key, out var item) ? item : null);
}