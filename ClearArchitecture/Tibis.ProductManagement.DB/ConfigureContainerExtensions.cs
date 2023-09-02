using Microsoft.Extensions.DependencyInjection;
using Tibis.Contracts.Interfaces;
using Tibis.ProductManagement.Domain;

namespace Tibis.ProductManagement.DB;

public static class ConfigureContainerExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<ProductRepository>();
        services.AddSingleton<ICreate<Product>>(x => x.GetRequiredService<ProductRepository>());
        services.AddSingleton<IRetrieveMany<Product>>(x => x.GetRequiredService<ProductRepository>());
        services.AddSingleton<IRetrieve<Guid, Product>>(x => x.GetRequiredService<ProductRepository>());
        services.AddSingleton<IRetrieve<string, Product>>(x => x.GetRequiredService<ProductRepository>());
    }
}