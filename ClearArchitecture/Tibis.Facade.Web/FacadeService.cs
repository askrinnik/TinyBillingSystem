using MediatR;
using Tibis.AccountManagement.CQRS.Requests;
using Tibis.Billing.CQRS.Requests;
using Tibis.Facade.Web.Models;
using Tibis.ProductManagement.CQRS.Models;
using Tibis.ProductManagement.CQRS.Requests;

namespace Tibis.Facade.Web;

public interface IFacadeService
{
    Task<DemoDataDto> CreateRcSubscriptionAsync(CancellationToken cancellationToken);
    Task<DemoDataDto> CreateUsageSubscriptionAsync(CancellationToken cancellationToken);
    Task CreateInvalidSubscriptionAsync(CancellationToken cancellationToken);
}

public class FacadeService : IFacadeService
{
    private readonly ILogger<FacadeService> _logger;
    private readonly ISender _sender;

    public FacadeService(ISender sender, ILogger<FacadeService> logger)
    {
        _sender = sender;
        _logger = logger;
    }

    public async Task<DemoDataDto> CreateRcSubscriptionAsync(CancellationToken cancellationToken)
    {   
        _logger.LogInformation("Creating an RC subscription");
        var rcProduct = await _sender.Send(new CreateProductRequest($"ProductRC_{Guid.NewGuid()}", ProductType.RecurringCharge, 2), cancellationToken);
        var account = await _sender.Send(new CreateAccountRequest($"User_{Guid.NewGuid()}"), cancellationToken);
        var subscription = await _sender.Send(new CreateSubscriptionRequest(rcProduct.Id, account.Id), cancellationToken);

        return new(rcProduct, account, subscription.Id);
    }

    public async Task<DemoDataDto> CreateUsageSubscriptionAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a usage subscription and meter");
        var usageProduct = await _sender.Send(new CreateProductRequest($"ProductUsage_{Guid.NewGuid()}", ProductType.Usage, 2), cancellationToken);
        var account = await _sender.Send(new CreateAccountRequest($"User_{Guid.NewGuid()}"), cancellationToken);
        var subscription = await _sender.Send(new CreateSubscriptionRequest(usageProduct.Id, account.Id), cancellationToken);

        await _sender.Send(new MeterUsageRequest(usageProduct.Name, account.Name, DateTime.Now, 2), cancellationToken);
        await _sender.Send(new MeterUsageRequest(usageProduct.Name, account.Name, DateTime.Now, 3), cancellationToken);
        await _sender.Send(new MeterUsageRequest(usageProduct.Name, account.Name, DateTime.Now, 4), cancellationToken);
        
        return new(usageProduct, account, subscription.Id);
    }

    public async Task CreateInvalidSubscriptionAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating invalid subscription");
        _ = await _sender.Send(new CreateSubscriptionRequest(Guid.NewGuid(), Guid.NewGuid()), cancellationToken);
    }
}