using Tibis.Domain;

namespace Tibis.Application.ProductManagement;

public class ProductAlreadyExistsException : ItemAlreadyExistsException
{
    public ProductAlreadyExistsException(string name) : base($"Product with name {name} already exists.")
    {
    }
}

public class ProductNotFoundException : ItemNotFoundException
{
    public ProductNotFoundException(Guid id) : base($"Product with id {id} was not found.")
    {
    }
}