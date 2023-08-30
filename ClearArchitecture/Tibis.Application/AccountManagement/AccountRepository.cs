using System.Collections.Concurrent;
using Tibis.Domain;
using Tibis.Domain.AccountManagement;
using Tibis.Domain.Interfaces;

namespace Tibis.Application.AccountManagement;

public class AccountRepository:
    IRetrieveMany<Account>,
    ICreate<Account>,
    IRetrieve<Guid, Account>,
    IRetrieve<string, Account>
{
    private readonly ConcurrentDictionary<Guid, Account> _items = new();

    public IAsyncEnumerable<Account> RetrieveManyAsync() => 
        _items.Values.ToAsyncEnumerable();

    public Task<Account> CreateAsync(Account item)
    {
        if (item.Id != Guid.Empty)
            throw new TibisValidationException("Id must be empty");

        if(_items.Values.Any(x => x.Name == item.Name))
            throw new AccountAlreadyExistsException(item.Name);

        var newItem = item with { Id = Guid.NewGuid() };
        _items.TryAdd(newItem.Id, newItem);
        return Task.FromResult(newItem);
    }

    public Task<Account?> TryRetrieveAsync(Guid key) => 
        Task.FromResult(_items.TryGetValue(key, out var item) ? item : null);

    public Task<Account?> TryRetrieveAsync(string key) =>
        Task.FromResult(_items.Values.SingleOrDefault(x => x.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase)));
}