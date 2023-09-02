using Tibis.Contracts.Exceptions;

namespace Tibis.AccountManagement.Domain.Exceptions;

public class AccountAlreadyExistsException : ItemAlreadyExistsException
{
    public AccountAlreadyExistsException(string name) : base($"Account with name {name} already exists.")
    {
    }
}