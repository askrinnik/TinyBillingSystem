using Microsoft.Extensions.Configuration;
using Tibis.Application.HttpClients;
using Tibis.Domain;
using Tibis.Domain.Billing;

namespace Tibis.Application.Billing.Services;

public class BillingHttpClient: IBillingClient
{
    private readonly HttpClients.BillingHttpClient _httpClient;

    public BillingHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        var url = configuration["BillingUrl"]
                  ?? throw new InvalidConfigurationException("BillingUrl");
        _httpClient = new(url, httpClient);
    }

    public async Task<Subscription> GetSubscriptionAsync(Guid id)
    {
        var subscription = await _httpClient.SubscriptionGETAsync(id);
        return new(subscription.Id, subscription.ProductId, subscription.AccountId);
    }

    public async Task<Subscription> GetSubscriptionAsync(Guid productId, Guid accountId)
    {
        var subscription = await _httpClient.AccountAsync(productId, accountId);
        return new(subscription.Id, subscription.ProductId, subscription.AccountId);
    }

    public async Task<Subscription> CreateSubscriptionAsync(Subscription subscription)
    {
        var createdSubscription = await _httpClient.SubscriptionPOSTAsync(new() { ProductId = subscription.ProductId, AccountId = subscription.AccountId});
        return new(createdSubscription.Id, createdSubscription.ProductId, createdSubscription.AccountId);
    }

    public async Task<AccountUsage> MeterUsageAsync(Queries.MeterUsageRequest meterUsageRequest)
    {
        var a = await _httpClient.MeterAsync(
            new MeterUsageRequest()
            {
                AccountName = meterUsageRequest.AccountName,
                ProductName = meterUsageRequest.ProductName,
                Date = meterUsageRequest.Date,
                Count = meterUsageRequest.Count
            });
        return new(a.Id, a.SubscriptionId, a.Date.DateTime, a.Amount);
    }
}