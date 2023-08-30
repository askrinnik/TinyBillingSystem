using Tibis.Application.AccountManagement.Models;
using Tibis.Application.AccountManagement.Services;
using Tibis.Application.Billing.Services;
using Tibis.Application.ProductManagement.Models;
using Tibis.Application.ProductManagement.Services;
using Tibis.Domain.ProductManagement;
using Tibis.Facade.Web.Models;

namespace Tibis.Facade.Web;

public interface IFacadeService
{
    Task<DemoDataDto> CreateRcSubscriptionAsync();
    Task<DemoDataDto> CreateUsageSubscriptionAsync();
    Task CreateInvalidSubscriptionAsync();
}

public class FacadeService : IFacadeService
{
    private readonly IAccountClient _accountClient;
    private readonly IProductClient _productClient;
    private readonly IBillingClient _billingClient;

    public FacadeService(IAccountClient accountClient, IProductClient productClient, IBillingClient billingClient)
    {
        _accountClient = accountClient;
        _productClient = productClient;
        _billingClient = billingClient;
    }

    public async Task<DemoDataDto> CreateRcSubscriptionAsync()
    {   
        var rcProduct = await _productClient.CreateProductAsync(new($"ProductRC_{Guid.NewGuid()}", ProductType.RecurringCharge, 2));
        var account = await _accountClient.CreateAsync(new($"User_{Guid.NewGuid()}"));
        var subscription = await _billingClient.CreateSubscriptionAsync(new(rcProduct.Id, account.Id));

        return new(ProductDto.From(rcProduct), AccountDto.From(account), subscription.Id);
    }

    public async Task<DemoDataDto> CreateUsageSubscriptionAsync()
    {
        var usageProduct = await _productClient.CreateProductAsync(new($"ProductUsage_{Guid.NewGuid()}", ProductType.Usage, 2));
        var account = await _accountClient.CreateAsync(new($"User_{Guid.NewGuid()}"));
        var subscription = await _billingClient.CreateSubscriptionAsync(new(usageProduct.Id, account.Id));

        await _billingClient.MeterUsageAsync(new(usageProduct.Name, account.Name, DateTime.Now, 2));
        await _billingClient.MeterUsageAsync(new(usageProduct.Name, account.Name, DateTime.Now, 3));
        await _billingClient.MeterUsageAsync(new(usageProduct.Name, account.Name, DateTime.Now, 4));
        
        return new(ProductDto.From(usageProduct), AccountDto.From(account), subscription.Id);
    }

    public async Task CreateInvalidSubscriptionAsync()
    {
        _ = await _billingClient.CreateSubscriptionAsync(new(Guid.NewGuid(), Guid.NewGuid()));
    }
}