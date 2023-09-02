using Microsoft.Extensions.Configuration;
using Tibis.Billing.CQRS.Models;
using Tibis.Billing.CQRS.Requests;
using Tibis.Contracts.Exceptions;

namespace Tibis.Billing.HttpClients;

internal class BillingHttpClient: IBillingClient
{
    private readonly OpenApi.BillingHttpClient _httpClient;

    public BillingHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        var url = configuration["BillingUrl"]
                  ?? throw new InvalidConfigurationException("BillingUrl");
        _httpClient = new(url, httpClient);
    }

    public async Task<SubscriptionDto> GetSubscriptionAsync(Guid id, CancellationToken cancellationToken)
    {
        var subscription = await _httpClient.SubscriptionGETAsync(id, cancellationToken);
        return new(subscription.Id, subscription.ProductId, subscription.AccountId);
    }

    public async Task<SubscriptionDto> GetSubscriptionAsync(Guid productId, Guid accountId, CancellationToken cancellationToken)
    {
        var subscription = await _httpClient.AccountAsync(productId, accountId, cancellationToken);
        return new(subscription.Id, subscription.ProductId, subscription.AccountId);
    }

    public async Task<SubscriptionDto> CreateSubscriptionAsync(CreateSubscriptionRequest subscription, CancellationToken cancellationToken)
    {
        var createdSubscription = await _httpClient.SubscriptionPOSTAsync(
            new() { ProductId = subscription.ProductId, AccountId = subscription.AccountId},
            cancellationToken);
        return new(createdSubscription.Id, createdSubscription.ProductId, createdSubscription.AccountId);
    }

    public async Task<AccountUsageDto> MeterUsageAsync(MeterUsageRequest meterUsageRequest, CancellationToken cancellationToken)
    {
        var usageDto = await _httpClient.MeterAsync(
            new()
            {
                AccountName = meterUsageRequest.AccountName,
                ProductName = meterUsageRequest.ProductName,
                Date = meterUsageRequest.Date,
                Count = meterUsageRequest.Count
            }, 
            cancellationToken);
        return new(usageDto.Id, usageDto.SubscriptionId, usageDto.Date.DateTime, usageDto.Amount);
    }

    public async Task<IEnumerable<AccountUsageDto>> GetAllAccountUsagesAsync(CancellationToken cancellationToken) =>
        (await _httpClient.AllAsync(cancellationToken)).Select(usage => 
            new AccountUsageDto(usage.Id, usage.SubscriptionId, usage.Date.DateTime, usage.Amount)).ToArray();

    public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync(CancellationToken cancellationToken) =>
        (await _httpClient.All2Async(cancellationToken)).Select(subscription => 
            new SubscriptionDto(subscription.Id, subscription.ProductId, subscription.AccountId)).ToArray();

    public async Task<SubscriptionDto> GetSubscriptionByProductIdAccountIdAsync(GetSubscriptionByProductIdAccountIdRequest request,
        CancellationToken cancellationToken)
    {
        var subscription = await _httpClient.AccountAsync(request.ProductId, request.AccountId, cancellationToken);
        return new(subscription.Id, subscription.ProductId, subscription.AccountId);
    }
}