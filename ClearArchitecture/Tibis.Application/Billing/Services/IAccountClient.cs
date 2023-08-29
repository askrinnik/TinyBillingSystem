using Tibis.Application.AccountManagement.Models;

namespace Tibis.Application.Billing.Services;

public interface IAccountClient
{
    Task<AccountDto> GetAccountAsync(Guid id);
}