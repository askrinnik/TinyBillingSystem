using Tibis.Application.HttpClients;
using Tibis.Facade.Web.Models;

namespace Tibis.Facade.Web;

public interface IFacadeService
{
    Task<DemoDataDto> CreateDemoDataAsync();
    Task CreateInvalidSubscriptionAsync();
}

public class FacadeService : IFacadeService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public FacadeService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<DemoDataDto> CreateDemoDataAsync()
    {   
        var prodClient = GetProductManagementHttpClient();

        var rcProduct = await prodClient.ProductPOSTAsync(new()
            { Name = $"Product_{Guid.NewGuid()}", ProductType = 0, Rate = 2 });

        var accClient = GetAccountManagementHttpClient();

        var account = await accClient.AccountPOSTAsync(new() { Name = $"User_{Guid.NewGuid()}" });

        var billingClient = GetBillingHttpClient();
        var subscription = await billingClient.SubscriptionPOSTAsync(new()
        {
            AccountId = account.Id,
            ProductId = rcProduct.Id
        });

        return new DemoDataDto(account.Id, rcProduct.Id, subscription.Id);
    }

    public async Task CreateInvalidSubscriptionAsync()
    {
        var billingClient = GetBillingHttpClient();
        var subscription = await billingClient.SubscriptionPOSTAsync(new()
        {
            AccountId = Guid.NewGuid(),
            ProductId = Guid.NewGuid()
        });
    }

    private BillingHttpClient GetBillingHttpClient()
    {
        var billingUrl = _configuration["BillingUrl"] ?? "http://localhost:5003";
        var billingClient = new BillingHttpClient(billingUrl, _httpClient);
        return billingClient;
    }

    private AccountManagementHttpClient GetAccountManagementHttpClient()
    {
        var accountManagementUrl = _configuration["AccountManagementUrl"] ?? "http://localhost:5001";
        var accClient = new AccountManagementHttpClient(accountManagementUrl, _httpClient);
        return accClient;
    }

    private ProductManagementHttpClient GetProductManagementHttpClient()
    {
        var productManagementUrl = _configuration["ProductManagementUrl"] ?? "http://localhost:5002";
        var prodClient = new ProductManagementHttpClient(productManagementUrl, _httpClient);
        return prodClient;
    }
}