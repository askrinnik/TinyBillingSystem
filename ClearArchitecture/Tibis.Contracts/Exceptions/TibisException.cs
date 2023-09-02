namespace Tibis.Contracts.Exceptions;

public class TibisException: Exception
{
    public TibisException(string message): base(message)
    { }
}