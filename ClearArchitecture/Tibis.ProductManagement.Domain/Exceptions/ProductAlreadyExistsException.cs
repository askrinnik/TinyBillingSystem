using Tibis.Contracts.Exceptions;

namespace Tibis.ProductManagement.Domain.Exceptions;

public class ProductAlreadyExistsException : ItemAlreadyExistsException
{
    public ProductAlreadyExistsException(string name) : base($"Product with name {name} already exists.")
    {
    }
}