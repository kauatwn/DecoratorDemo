using Domain.Entities;

namespace Domain.Interfaces;

public interface IProductRepository
{
    void Add(Product product);
    Task<IReadOnlyCollection<Product>> GetAllAsync();
}