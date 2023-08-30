using Tibis.Domain.AccountManagement;

namespace Tibis.Application.AccountManagement.Services;

public interface IAccountClient
{
    Task<Account> GetAccountAsync(Guid id);
    Task<Account> GetAccountAsync(string name);
    Task<Account> CreateAsync(Account account);
}