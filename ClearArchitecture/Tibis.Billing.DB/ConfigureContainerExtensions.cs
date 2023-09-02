using Microsoft.Extensions.DependencyInjection;
using Tibis.Billing.Domain;
using Tibis.Contracts.Interfaces;

namespace Tibis.Billing.DB;

public static class ConfigureContainerExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<SubscriptionRepository>();
        services.AddSingleton<ICreate<Subscription>>(x => x.GetRequiredService<SubscriptionRepository>());
        services.AddSingleton<IRetrieveMany<Subscription>>(x => x.GetRequiredService<SubscriptionRepository>());
        services.AddSingleton<IRetrieve<Guid, Subscription>>(x => x.GetRequiredService<SubscriptionRepository>());
        services.AddSingleton<IRetrieve<Guid, Guid, Subscription>>(x => x.GetRequiredService<SubscriptionRepository>());

        services.AddSingleton<AccountUsageRepository>();
        services.AddSingleton<ICreate<AccountUsage>>(x => x.GetRequiredService<AccountUsageRepository>());
        services.AddSingleton<IRetrieveMany<AccountUsage>>(x => x.GetRequiredService<AccountUsageRepository>());
        services.AddSingleton<IRetrieve<Guid, AccountUsage>>(x => x.GetRequiredService<AccountUsageRepository>());
    }
}