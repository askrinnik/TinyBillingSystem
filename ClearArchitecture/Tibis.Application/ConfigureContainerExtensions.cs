using Microsoft.Extensions.DependencyInjection;
using Tibis.Application.AccountManagement;
using Tibis.Application.ProductManagement;
using Tibis.Domain.AccountManagement;
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

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<AssemblyMarker>());
    }
}