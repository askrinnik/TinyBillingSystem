using Microsoft.Extensions.DependencyInjection;

namespace Tibis.Billing.HttpClients;

public static class ConfigureContainerExtensions
{
    public static void AddBillingHandlers(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<AssemblyMarker>());
        services.AddHttpClient<IBillingClient, BillingHttpClient>();

    }
}