using DecoratorDemo.Domain.Entities;
using DecoratorDemo.Domain.Interfaces.Repositories;
using DecoratorDemo.Application.Interfaces.UseCases;

namespace DecoratorDemo.Application.UseCases;

public class GetAllProductsUseCase(IProductRepository repository) : IGetAllProductsUseCase
{
    public async Task<IEnumerable<Product>> Execute() => await repository.GetAllAsync();
}