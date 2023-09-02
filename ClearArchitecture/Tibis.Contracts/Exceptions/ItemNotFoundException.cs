namespace Tibis.Contracts.Exceptions;

public class ItemNotFoundException : TibisException
{
    public ItemNotFoundException(string message) : base(message)
    { }
}