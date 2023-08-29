using Tibis.Application.AccountManagement.Services;
using Tibis.Application.Billing.Services;
using Tibis.Application.ProductManagement.Services;
using Tibis.Domain.ProductManagement;
using Tibis.Facade.Web.Models;

namespace Tibis.Facade.Web;

public interface IFacadeService
{
    Task<DemoDataDto> CreateDemoDataAsync();
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

    public async Task<DemoDataDto> CreateDemoDataAsync()
    {   
        var rcProduct = await _productClient.CreateProductAsync(new($"Product_{Guid.NewGuid()}", ProductType.RecurringCharge, 2));
        var account = await _accountClient.CreateAsync(new($"User_{Guid.NewGuid()}"));
        var subscription = await _billingClient.CreateSubscriptionAsync(new(rcProduct.Id, account.Id));
        return new(account.Id, rcProduct.Id, subscription.Id);
    }

    public async Task CreateInvalidSubscriptionAsync()
    {
        _ = await _billingClient.CreateSubscriptionAsync(new(Guid.NewGuid(), Guid.NewGuid()));
    }
}