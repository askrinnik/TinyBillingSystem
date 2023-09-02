using Microsoft.Extensions.DependencyInjection;

namespace Tibis.ProductManagement.HttpClients;

public static class ConfigureContainerExtensions
{
    public static void AddProductManagementHandlers(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<AssemblyMarker>());
        services.AddHttpClient<IProductManagementClient, ProductHttpClient>();

    }
}