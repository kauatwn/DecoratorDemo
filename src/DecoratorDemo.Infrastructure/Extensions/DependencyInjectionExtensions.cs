using DecoratorDemo.Application.Interfaces.Services;
using DecoratorDemo.Domain.Interfaces.Repositories;
using DecoratorDemo.Infrastructure.Context;
using DecoratorDemo.Infrastructure.Decorators;
using DecoratorDemo.Infrastructure.Repositories;
using DecoratorDemo.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DecoratorDemo.Infrastructure.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        AddInMemoryDbContext(services);
        AddCaching(services);
        AddRepositories(services);
    }

    private static void AddInMemoryDbContext(IServiceCollection services)
    {
        services.AddDbContext<InMemoryAppDbContext>(options => options.UseInMemoryDatabase("ProductsDb"));
    }

    private static void AddCaching(IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<ICacheService, MemoryCacheService>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IProductRepository>(provider =>
        {
            var context = provider.GetRequiredService<InMemoryAppDbContext>();
            ProductRepository repository = new(context);

            var cache = provider.GetRequiredService<ICacheService>();
            return new CachedProductRepository(repository, cache);
        });
    }
}