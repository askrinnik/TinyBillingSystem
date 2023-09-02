using Tibis.AccountManagement.CQRS.Requests;

namespace Tibis.AccountManagement.HttpClients;

internal interface IAccountManagementClient
{
    Task<CQRS.Models.AccountDto> GetAccountAsync(Guid id, CancellationToken cancellationToken);
    Task<CQRS.Models.AccountDto> GetAccountAsync(string name, CancellationToken cancellationToken);
    Task<CQRS.Models.AccountDto> CreateAsync(CreateAccountRequest account, CancellationToken cancellationToken);
}