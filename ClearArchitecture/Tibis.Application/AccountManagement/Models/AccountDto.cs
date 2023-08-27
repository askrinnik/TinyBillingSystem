using Tibis.Domain.AccountManagement;

namespace Tibis.Application.AccountManagement.Models;

public record AccountDto(Guid Id, string Name)
{
    public static AccountDto From(Account account) => new(account.Id, account.Name);
}