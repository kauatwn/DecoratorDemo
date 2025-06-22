using DecoratorDemo.Domain.Entities;

namespace DecoratorDemo.Application.Interfaces.UseCases;

public interface IGetAllProductsUseCase
{
    Task<IEnumerable<Product>> Execute();
}