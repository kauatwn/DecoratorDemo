using DecoratorDemo.Application.Interfaces.Services;
using DecoratorDemo.Domain.Interfaces.Repositories;
using DecoratorDemo.Infrastructure.Decorators;
using DecoratorDemo.Infrastructure.Repositories;
using DecoratorDemo.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DecoratorDemo.Infrastructure.Extensions;

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