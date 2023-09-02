using Microsoft.Extensions.DependencyInjection;
using Tibis.AccountManagement.Domain;
using Tibis.Contracts.Interfaces;

namespace Tibis.AccountManagement.DB;

public static class ConfigureContainerExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<AccountRepository>();
        services.AddSingleton<ICreate<Account>>(x => x.GetRequiredService<AccountRepository>());
        services.AddSingleton<IRetrieveMany<Account>>(x => x.GetRequiredService<AccountRepository>());
        services.AddSingleton<IRetrieve<Guid, Account>>(x => x.GetRequiredService<AccountRepository>());
        services.AddSingleton<IRetrieve<string, Account>>(x => x.GetRequiredService<AccountRepository>());
    }
}