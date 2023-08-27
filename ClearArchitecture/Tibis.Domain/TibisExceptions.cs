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

public class AccountAlreadyExistsException : ItemAlreadyExistsException
{
    public AccountAlreadyExistsException(string name) : base($"Account with name {name} already exists.")
    {
    }
}

public class AccountNotFoundException : ItemNotFoundException
{
    public AccountNotFoundException(Guid id) : base($"Account with id {id} was not found.")
    {
    }
}