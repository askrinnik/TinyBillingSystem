using Tibis.Contracts.Exceptions;

namespace Tibis.ProductManagement.Domain.Exceptions;

public class InvalidProductTypeException : TibisValidationException
{
    public InvalidProductTypeException(string name) : base($"Product with name '{name}' is not Usage type.")
    {
    }
}