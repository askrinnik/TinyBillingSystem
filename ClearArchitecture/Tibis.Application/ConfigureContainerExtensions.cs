using Microsoft.Extensions.DependencyInjection;
using Tibis.Application.AccountManagement;
using Tibis.Application.Billing;
using Tibis.Application.Billing.Services;
using Tibis.Application.ProductManagement;
using Tibis.Domain.AccountManagement;
using Tibis.Domain.Billing;
using Tibis.Domain.Interfaces;
using Tibis.Domain.ProductManagement;

namespace Tibis.Application;

public static class ConfigureContainerExtensions
{
    public static void AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddSingleton<AccountRepository>();
        services.AddSingleton<ICreate<Account>>(x => x.GetRequiredService<AccountRepository>());
        services.AddSingleton<IRetrieveMany<Account>>(x => x.GetRequiredService<AccountRepository>());
        services.AddSingleton<IRetrieve<Guid, Account>>(x => x.GetRequiredService<AccountRepository>());

        services.AddSingleton<ProductRepository>();
        services.AddSingleton<ICreate<Product>>(x => x.GetRequiredService<ProductRepository>());
        services.AddSingleton<IRetrieveMany<Product>>(x => x.GetRequiredService<ProductRepository>());
        services.AddSingleton<IRetrieve<Guid, Product>>(x => x.GetRequiredService<ProductRepository>());

        services.AddSingleton<SubscriptionRepository>();
        services.AddSingleton<ICreate<Subscription>>(x => x.GetRequiredService<SubscriptionRepository>());
        services.AddSingleton<IRetrieveMany<Subscription>>(x => x.GetRequiredService<SubscriptionRepository>());
        services.AddSingleton<IRetrieve<Guid, Subscription>>(x => x.GetRequiredService<SubscriptionRepository>());

        services.AddHttpClient<IProductClient, ProductHttpClient>();
        services.AddHttpClient<IAccountClient, AccountHttpClient>();

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<AssemblyMarker>());
    }
}