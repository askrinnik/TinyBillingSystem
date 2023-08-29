using Microsoft.Extensions.Configuration;
using Tibis.Application.HttpClients;

namespace Tibis.Application.Billing.Services;

public class AccountHttpClient: IAccountClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public AccountHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<AccountManagement.Models.AccountDto> GetAccountAsync(Guid id)
    {
        var accountManagementUrl = _configuration["AccountManagementUrl"] ?? "http://localhost:5002";
        var prodClient = new AccountManagementHttpClient(accountManagementUrl, _httpClient);
        var account = await prodClient.AccountGETAsync(id);
        return new(account.Id, account.Name);
    }
}