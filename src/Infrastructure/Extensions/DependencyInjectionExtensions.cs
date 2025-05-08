using Domain.Interfaces;
using Infrastructure.Decorators;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        AddCaching(services);
        AddRepositories(services);
    }

    private static void AddCaching(IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<ICacheService, MemoryCacheService>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddSingleton<IProductRepository>(provider =>
        {
            var repository = new ProductRepository();
            var cache = provider.GetRequiredService<ICacheService>();

            return new CachedProductRepository(repository, cache);
        });
    }
}