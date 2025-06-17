using Decorator_Demo.Domain.Entities;

namespace Decorator_Demo.Application.Interfaces.UseCases;

public interface IGetAllProductsUseCase
{
    Task<IEnumerable<Product>> Execute();
}