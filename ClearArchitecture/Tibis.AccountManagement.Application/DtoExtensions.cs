using Tibis.AccountManagement.CQRS.Models;
using Tibis.AccountManagement.Domain;

namespace Tibis.AccountManagement.Application;

internal static class DtoExtensions
{
    public static AccountDto ToDto(this Account account) => new(account.Id, account.Name);
}