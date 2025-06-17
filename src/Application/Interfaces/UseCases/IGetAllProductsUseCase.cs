using Domain.Entities;

namespace Application.Interfaces.UseCases;

public interface IGetAllProductsUseCase
{
    Task<IEnumerable<Product>> Execute();
}