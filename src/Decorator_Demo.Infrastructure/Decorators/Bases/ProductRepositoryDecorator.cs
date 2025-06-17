using Decorator_Demo.Domain.Entities;
using Decorator_Demo.Domain.Interfaces.Repositories;

namespace Decorator_Demo.Infrastructure.Decorators.Bases;

public abstract class ProductRepositoryDecorator(IProductRepository decorated) : IProductRepository
{
    public virtual void Add(Product product) => decorated.Add(product);

    public virtual async Task<IReadOnlyCollection<Product>> GetAllAsync() => await decorated.GetAllAsync();
}