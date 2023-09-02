using Microsoft.Extensions.DependencyInjection;

namespace Tibis.Billing.Application;

public static class ConfigureContainerExtensions
{
    public static void AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<AssemblyMarker>());
    }
}