using Tibis.Contracts.Exceptions;

namespace Tibis.AccountManagement.Domain.Exceptions;

public class AccountNotFoundException : ItemNotFoundException
{
    public AccountNotFoundException(Guid id) : base($"Account with id {id} was not found.")
    {
    }

    public AccountNotFoundException(string name) : base($"Account with name {name} was not found.")
    {
    }
}