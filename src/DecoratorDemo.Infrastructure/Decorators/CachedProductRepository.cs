using DecoratorDemo.Application.Interfaces.Services;
using DecoratorDemo.Domain.Entities;
using DecoratorDemo.Domain.Interfaces.Repositories;
using DecoratorDemo.Infrastructure.Decorators.Bases;

namespace DecoratorDemo.Infrastructure.Decorators;

public class CachedProductRepository(IProductRepository decorated, ICacheService cache)
    : ProductRepositoryDecorator(decorated)
{
    private const string AllProductsCacheKey = "all-products";

    public override void Add(Product product)
    {
        base.Add(product);

        cache.Remove(AllProductsCacheKey);
        Console.WriteLine("--> Cache de produtos removido devido à adição de um novo produto.");
    }

    public override async Task<IReadOnlyCollection<Product>> GetAllAsync()
    {
        if (cache.Get<IReadOnlyCollection<Product>>(AllProductsCacheKey) is { } cachedProducts)
        {
            Console.WriteLine("--> Retornando lista de produtos do cache...");

            return cachedProducts;
        }

        IReadOnlyCollection<Product> products = await base.GetAllAsync();
        cache.Set(AllProductsCacheKey, products, TimeSpan.FromSeconds(30));

        return products;
    }
}