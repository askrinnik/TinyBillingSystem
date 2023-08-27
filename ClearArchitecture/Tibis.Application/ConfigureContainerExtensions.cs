using Microsoft.Extensions.DependencyInjection;
using Tibis.Application.AccountManagement;
using Tibis.Domain.AccountManagement;
using Tibis.Domain.Interfaces;

namespace Tibis.Application;

public static class ConfigureContainerExtensions
{
    public static void AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddSingleton<AccountRepository>();
        services.AddSingleton<ICreate<Account>>(x => x.GetRequiredService<AccountRepository>());
        services.AddSingleton<IRetrieveMany<Account>>(x => x.GetRequiredService<AccountRepository>());
        services.AddSingleton<IRetrieve<Guid, Account>>(x => x.GetRequiredService<AccountRepository>());

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<AssemblyMarker>());
    }
}