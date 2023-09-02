using System.Collections.Concurrent;
using Tibis.Contracts.Exceptions;
using Tibis.Contracts.Interfaces;
using Tibis.ProductManagement.Domain;
using Tibis.ProductManagement.Domain.Exceptions;

namespace Tibis.ProductManagement.DB;

public class ProductRepository:
    IRetrieveMany<Product>,
    ICreate<Product>,
    IRetrieve<Guid, Product>,
    IRetrieve<string, Product>
{
    private readonly ConcurrentDictionary<Guid, Product> _items = new();

    public IAsyncEnumerable<Product> RetrieveManyAsync() => 
        _items.Values.ToAsyncEnumerable();

    public Task<Product> CreateAsync(Product item)
    {
        if (item.Id != Guid.Empty)
            throw new TibisValidationException("Id must be empty");

        if(_items.Values.Any(x => x.Name == item.Name))
            throw new ProductAlreadyExistsException(item.Name);

        var newItem = item with { Id = Guid.NewGuid() };
        _items.TryAdd(newItem.Id, newItem);
        return Task.FromResult(newItem);
    }

    public Task<Product?> TryRetrieveAsync(Guid key) => 
        Task.FromResult(_items.TryGetValue(key, out var item) ? item : null);

    public Task<Product?> TryRetrieveAsync(string key) => 
        Task.FromResult(_items.Values.SingleOrDefault(x => x.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase)));
}