using DecoratorDemo.Domain.Entities;
using DecoratorDemo.Domain.Interfaces.Repositories;

namespace DecoratorDemo.Infrastructure.Decorators.Bases;

public abstract class ProductRepositoryDecorator(IProductRepository decorated) : IProductRepository
{
    public virtual void Add(Product product) => decorated.Add(product);

    public virtual async Task<IReadOnlyCollection<Product>> GetAllAsync() => await decorated.GetAllAsync();
}