using Microsoft.Extensions.Configuration;
using Tibis.AccountManagement.CQRS.Models;
using Tibis.AccountManagement.CQRS.Requests;
using Tibis.Contracts.Exceptions;

namespace Tibis.AccountManagement.HttpClients;

internal class AccountHttpClient : IAccountManagementClient
{
    private readonly OpenApi.AccountManagementHttpClient _httpClient;

    public AccountHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        var url = configuration["AccountManagementUrl"]
                                   ?? throw new InvalidConfigurationException("AccountManagementUrl");
        _httpClient = new(url, httpClient);
    }

    public async Task<AccountDto> GetAccountAsync(Guid id, CancellationToken cancellationToken)
    {
        var account = await _httpClient.AccountGETAsync(id, cancellationToken);
        return new(account.Id, account.Name);
    }

    public async Task<AccountDto> GetAccountAsync(string name, CancellationToken cancellationToken)
    {
        var account = await _httpClient.NameAsync(name, cancellationToken);
        return new(account.Id, account.Name);
    }

    public async Task<AccountDto> CreateAsync(CreateAccountRequest account, CancellationToken cancellationToken)
    {
        var createdAccount = await _httpClient.AccountPOSTAsync(new() { Name = account.Name }, cancellationToken);
        return new(createdAccount.Id, createdAccount.Name);
    }
}