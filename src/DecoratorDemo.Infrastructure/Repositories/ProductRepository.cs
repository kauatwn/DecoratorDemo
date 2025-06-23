using DecoratorDemo.Domain.Entities;
using DecoratorDemo.Domain.Interfaces.Repositories;
using DecoratorDemo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DecoratorDemo.Infrastructure.Repositories;

public class ProductRepository(InMemoryAppDbContext context) : IProductRepository
{
    public void Add(Product product)
    {
        context.Products.Add(product);
        context.SaveChanges();
    }

    public async Task<IReadOnlyCollection<Product>> GetAllAsync()
    {
        Console.WriteLine("--> Simulando latência de banco real...");
        await Task.Delay(TimeSpan.FromSeconds(2));

        return await context.Products.AsNoTracking().ToListAsync();
    }
}