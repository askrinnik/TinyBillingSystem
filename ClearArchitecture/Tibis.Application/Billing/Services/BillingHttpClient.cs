using Microsoft.Extensions.Configuration;
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

    public async Task<Subscription> CreateSubscriptionAsync(Subscription subscription)
    {
        var createdSubscription = await _httpClient.SubscriptionPOSTAsync(new() { ProductId = subscription.ProductId, AccountId = subscription.AccountId});
        return new(createdSubscription.Id, createdSubscription.ProductId, createdSubscription.AccountId);
    }
}