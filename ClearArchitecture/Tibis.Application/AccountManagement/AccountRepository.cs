using System.Collections.Concurrent;
using System.Security.Principal;
using Tibis.Domain;
using Tibis.Domain.AccountManagement;
using Tibis.Domain.Interfaces;

namespace Tibis.Application.AccountManagement;

public class AccountRepository:
    IRetrieveMany<Account>,
    ICreate<Account>,
    IRetrieve<Guid, Account>
{
    private readonly ConcurrentDictionary<Guid, Account> _accounts = new();

    public IAsyncEnumerable<Account> RetrieveManyAsync() => 
        _accounts.Values.ToAsyncEnumerable();

    public Task<Account> CreateAsync(Account item)
    {
        if (item.Id != Guid.Empty)
            throw new TibisValidationException("Id must be empty");

        if(_accounts.Values.Any(x => x.Name == item.Name))
            throw new AccountAlreadyExistsException(item.Name);

        var newItem = item with { Id = Guid.NewGuid() };
        _accounts.TryAdd(newItem.Id, newItem);
        return Task.FromResult(newItem);
    }

    public Task<Account?> TryRetrieveAsync(Guid key) => 
        Task.FromResult(_accounts.TryGetValue(key, out var item) ? item : null);
}