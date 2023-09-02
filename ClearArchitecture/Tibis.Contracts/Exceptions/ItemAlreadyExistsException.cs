namespace Tibis.Contracts.Exceptions;

public class ItemAlreadyExistsException : TibisException
{
    public ItemAlreadyExistsException(string message) : base(message)
    { }
}