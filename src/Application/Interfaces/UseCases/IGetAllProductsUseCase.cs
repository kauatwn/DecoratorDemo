using Domain.Entities;

namespace Application.Abstractions;

public interface IGetAllProductsUseCase
{
    Task<IEnumerable<Product>> Execute();
}