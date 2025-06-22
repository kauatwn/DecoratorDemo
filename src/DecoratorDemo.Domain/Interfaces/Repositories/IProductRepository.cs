using DecoratorDemo.Domain.Entities;

namespace DecoratorDemo.Domain.Interfaces.Repositories;

public interface IProductRepository
{
    void Add(Product product);
    Task<IReadOnlyCollection<Product>> GetAllAsync();
}