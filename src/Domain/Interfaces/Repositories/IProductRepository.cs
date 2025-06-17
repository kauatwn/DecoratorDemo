using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IProductRepository
{
    void Add(Product product);
    Task<IReadOnlyCollection<Product>> GetAllAsync();
}