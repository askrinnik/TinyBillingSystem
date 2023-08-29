using Tibis.Domain.AccountManagement;

namespace Tibis.Application.AccountManagement.Services;

public interface IAccountClient
{
    Task<Account> GetAccountAsync(Guid id);
    Task<Account> CreateAsync(Account account);
}