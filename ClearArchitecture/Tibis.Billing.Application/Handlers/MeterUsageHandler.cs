using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Tibis.AccountManagement.CQRS.Requests;
using Tibis.Billing.CQRS.Models;
using Tibis.Billing.CQRS.Requests;
using Tibis.Billing.Domain;
using Tibis.Billing.Domain.Exceptions;
using Tibis.Contracts.Exceptions;
using Tibis.Contracts.Interfaces;
using Tibis.ProductManagement.CQRS.Models;
using Tibis.ProductManagement.CQRS.Requests;

namespace Tibis.Billing.Application.Handlers;

internal class MeterUsageHandler: IRequestHandler<MeterUsageRequest, AccountUsageDto>
{
    private readonly ICreate<AccountUsage> _accountUsageRepository;
    private readonly IRetrieve<Guid, Guid, Subscription> _subscriptionRepository;
    private readonly ILogger<MeterUsageHandler> _logger;
    private readonly ISender _sender;

    public MeterUsageHandler(
        ICreate<AccountUsage> accountUsageRepository, 
        IRetrieve<Guid, Guid, Subscription> subscriptionRepository, 
        ILogger<MeterUsageHandler> logger, ISender sender)
    {
        _accountUsageRepository = accountUsageRepository;
        _subscriptionRepository = subscriptionRepository;
        _logger = logger;
        _sender = sender;
    }

    public async Task<AccountUsageDto> Handle(MeterUsageRequest request, CancellationToken cancellationToken)
    {
        var pipeline = CreatePipeline();

        var session = new Session();
        await InitializeSession(request, session);

        using (var slow = Activity.Current?.Source.StartActivity("ProcessUsage"))
        {
            foreach (var plugin in pipeline)
                await plugin(session, cancellationToken);
        }

        return session.AccountUsage!.ToDto();
    }

    private List<Func<Session, CancellationToken, Task>> CreatePipeline() =>
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

    private async Task GetProductDetails(Session session, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting product details for product {ProductName}", session.ProductName);
        var product = await _sender.Send(new GetProductByNameRequest(session.ProductName!), cancellationToken);
        session.ProductId = product.Id;
        session.Rate = product.Rate;
        session.ProductType = product.ProductType;
    }

    private Task ValidateUsageProduct(Session session, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validating product {ProductName} is a usage product", session.ProductName);
        if (session.ProductType != ProductType.Usage)
            throw new TibisValidationException($"Product with name '{session.ProductName!}' is not Usage type");
        return Task.CompletedTask;
    }

    private async Task GetAccountDetails(Session session, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting account details for account {AccountName}", session.AccountName);
        var account = await _sender.Send(new GetAccountByNameRequest(session.AccountName!), cancellationToken);
        session.AccountId = account.Id;
    }

    private async Task GetSubscriptionDetails(Session session, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting subscription details for product {ProductId} and account {AccountId}", session.ProductId, session.AccountId);
        var subscription = await _subscriptionRepository.TryRetrieveAsync(session.ProductId, session.AccountId) ??
        throw new SubscriptionNotFoundException(session.ProductId, session.AccountId);
        session.SubscriptionId = subscription.Id;
    }

    private Task CalculateAmount(Session session, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Calculating amount for rate {Rate}, count {UsageCount}", session.Rate, session.UsageCount);
        session.Amount = session.Rate * session.UsageCount;
        _logger.LogInformation("Calculated amount {Amount}", session.Amount);

        var myTags = new Dictionary<string, object?> { { "CalculatedAmount", session.Amount } };
        Activity.Current?.AddEvent(new ActivityEvent("Amount was generated", tags: new ActivityTagsCollection(myTags)));

        return Task.CompletedTask;
    }

    private async Task CreateAccountUsage(Session session, CancellationToken cancellationToken)
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