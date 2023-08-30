using Microsoft.Extensions.Configuration;
using Tibis.Application.HttpClients;
using Tibis.Domain;
using Tibis.Domain.AccountManagement;

namespace Tibis.Application.AccountManagement.Services;

public class AccountHttpClient : IAccountClient
{
    private readonly AccountManagementHttpClient _httpClient;

    public AccountHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        var url = configuration["AccountManagementUrl"]
                                   ?? throw new InvalidConfigurationException("AccountManagementUrl");
        _httpClient = new(url, httpClient);
    }

    public async Task<Account> GetAccountAsync(Guid id)
    {
        var account = await _httpClient.AccountGETAsync(id);
        return new(account.Id, account.Name);
    }

    public async Task<Account> GetAccountAsync(string name)
    {
        var account = await _httpClient.NameAsync(name);
        return new(account.Id, account.Name);
    }

    public async Task<Account> CreateAsync(Account account)
    {
        var createdAccount = await _httpClient.AccountPOSTAsync(new() { Name = account.Name });
        return new(createdAccount.Id, createdAccount.Name);
    }
}