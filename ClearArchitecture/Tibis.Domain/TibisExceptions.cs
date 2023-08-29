namespace Tibis.Domain;

public class TibisException: Exception
{
    public TibisException(string message): base(message)
    { }
}

public class TibisValidationException : TibisException
{
    public TibisValidationException(string message) : base(message)
    { }
}

public class ItemNotFoundException : TibisException
{
    public ItemNotFoundException(string message) : base(message)
    { }
}

public class ItemAlreadyExistsException : TibisException
{
    public ItemAlreadyExistsException(string message) : base(message)
    { }
}
public class InvalidConfigurationException : TibisException
{
    public InvalidConfigurationException(string configurationKey) : base($"A configuration value with key {configurationKey} was not found.")
    { }
}