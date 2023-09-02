using Microsoft.Extensions.DependencyInjection;

namespace Tibis.AccountManagement.HttpClients;

public static class ConfigureContainerExtensions
{
    public static void AddAccountManagementHandlers(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<AssemblyMarker>());
        services.AddHttpClient<IAccountManagementClient, AccountHttpClient>();

    }
}