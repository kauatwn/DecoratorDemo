using Decorator_Demo.Application.Interfaces.Services;
using Decorator_Demo.Infrastructure.Decorators;
using Decorator_Demo.Infrastructure.Repositories;
using Decorator_Demo.Infrastructure.Services;
using Decorator_Demo.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Decorator_Demo.Infrastructure.Extensions;

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