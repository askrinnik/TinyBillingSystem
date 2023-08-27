using Tibis.Domain;

namespace Tibis.Application.AccountManagement;

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