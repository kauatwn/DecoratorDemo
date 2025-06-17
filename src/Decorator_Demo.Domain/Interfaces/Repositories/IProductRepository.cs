using Decorator_Demo.Domain.Entities;

namespace Decorator_Demo.Domain.Interfaces.Repositories;

public interface IProductRepository
{
    void Add(Product product);
    Task<IReadOnlyCollection<Product>> GetAllAsync();
}