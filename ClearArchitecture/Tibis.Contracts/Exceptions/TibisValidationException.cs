namespace Tibis.Contracts.Exceptions;

public class TibisValidationException : TibisException
{
    public TibisValidationException(string message) : base(message)
    { }
}