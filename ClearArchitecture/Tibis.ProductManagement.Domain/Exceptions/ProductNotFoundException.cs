using Tibis.Contracts.Exceptions;

namespace Tibis.ProductManagement.Domain.Exceptions;

public class ProductNotFoundException : ItemNotFoundException
{
    public ProductNotFoundException(Guid id) : base($"Product with id {id} was not found.")
    {
    }
    public ProductNotFoundException(string name) : base($"Product with name '{name}' was not found.")
    {
    }
}