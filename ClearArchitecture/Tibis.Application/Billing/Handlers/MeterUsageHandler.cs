using MediatR;
using Microsoft.Extensions.Logging;
using Tibis.Application.AccountManagement;
using Tibis.Application.AccountManagement.Services;
using Tibis.Application.Billing.Models;
using Tibis.Application.Billing.Queries;
using Tibis.Application.ProductManagement;
using Tibis.Application.ProductManagement.Services;
using Tibis.Domain;
using Tibis.Domain.Billing;
using Tibis.Domain.Interfaces;
using Tibis.Domain.ProductManagement;

namespace Tibis.Application.Billing.Handlers;

public class MeterUsageHandler: IRequestHandler<MeterUsageRequest, AccountUsageDto>
{
    private readonly IProductClient _productClient;
    private readonly IAccountClient _accountClient;
    private readonly ICreate<AccountUsage> _accountUsageRepository;
    private readonly IRetrieve<Guid, Guid, Subscription> _subscriptionRepository;
    private readonly ILogger<MeterUsageHandler> _logger;

    public MeterUsageHandler(
        IProductClient productClient,
        IAccountClient accountClient, 
        ICreate<AccountUsage> accountUsageRepository, 
        IRetrieve<Guid, Guid, Subscription> subscriptionRepository, 
        ILogger<MeterUsageHandler> logger)
    {
        _productClient = productClient;
        _accountClient = accountClient;
        _accountUsageRepository = accountUsageRepository;
        _subscriptionRepository = subscriptionRepository;
        _logger = logger;
    }

    public async Task<AccountUsageDto> Handle(MeterUsageRequest request, CancellationToken cancellationToken)
    {
        var pipeline = CreatePipeline();

        var session = new Session();
        await InitializeSession(request, session);

        foreach (var plugin in pipeline) 
            await plugin(session);

        return AccountUsageDto.From(session.AccountUsage!);
    }

    private List<Func<Session, Task>> CreatePipeline() =>
        new()
        {
            GetProductDetails,
            ValidateUsageProduct,
            GetAccountDetails,
            GetSubscriptionDetails,
            CalculateAmount,
            CreateAccountUsage
        };

    private Task InitializeSession(MeterUsageRequest request, Session session)
    {
        _logger.LogInformation("Initializing session for product {ProductName}, account {AccountName}, date {Date}, count {Count}", request.ProductName, request.AccountName, request.Date, request.Count);
        session.ProductName = request.ProductName;
        session.AccountName = request.AccountName;
        session.UsageDate = request.Date;
        session.UsageCount = request.Count;
        return Task.CompletedTask;
    }

    private async Task GetProductDetails(Session session)
    {
        _logger.LogInformation("Getting product details for product {ProductName}", session.ProductName);
        var product = await _productClient.GetProductAsync(session.ProductName!) ?? throw new ProductNotFoundException(session.ProductName!);
        session.ProductId = product.Id;
        session.Rate = product.Rate;
        session.ProductType = product.ProductType;
    }

    private Task ValidateUsageProduct(Session session)
    {
        _logger.LogInformation("Validating product {ProductName} is a usage product", session.ProductName);
        if (session.ProductType != ProductType.Usage)
            throw new InvalidProductTypeException(session.ProductName!);
        return Task.CompletedTask;
    }

    private async Task GetAccountDetails(Session session)
    {
        _logger.LogInformation("Getting account details for account {AccountName}", session.AccountName);
        var account = await _accountClient.GetAccountAsync(session.AccountName!) ?? throw new AccountNotFoundException(session.AccountName!);
        session.AccountId = account.Id;
    }

    private async Task GetSubscriptionDetails(Session session)
    {
        _logger.LogInformation("Getting subscription details for product {ProductId} and account {AccountId}", session.ProductId, session.AccountId);
        var subscription = await _subscriptionRepository.TryRetrieveAsync(session.ProductId, session.AccountId) ??
        throw new SubscriptionNotFoundException(session.ProductId, session.AccountId);
        session.SubscriptionId = subscription.Id;
    }

    private Task CalculateAmount(Session session)
    {
        _logger.LogInformation("Calculating amount for rate {Rate}, count {UsageCount}", session.Rate, session.UsageCount);
        session.Amount = session.Rate * session.UsageCount;
        _logger.LogInformation("Calculated amount {Amount}", session.Amount);
        return Task.CompletedTask;
    }

    private async Task CreateAccountUsage(Session session)
    {
        _logger.LogInformation("Creating account usage for subscription {SubscriptionId}, date {UsageDate}, amount {Amount}", session.SubscriptionId, session.UsageDate, session.Amount);
        var accountUsage = new AccountUsage(session.SubscriptionId, session.UsageDate, session.Amount);
        var newUsage = await _accountUsageRepository.CreateAsync(accountUsage);
        session.AccountUsage = newUsage;
    }
}

internal class Session
{
    public Guid ProductId { get; set; }
    public int Rate { get; set; }
    public Guid AccountId { get; set; }
    public Guid SubscriptionId { get; set; }
    public DateTime UsageDate { get; set; }
    public int UsageCount { get; set; }
    public int Amount { get; set; }
    public string? ProductName { get; set; }
    public string? AccountName { get; set; }
    public AccountUsage? AccountUsage { get; set; }
    public ProductType ProductType { get; set; }
}