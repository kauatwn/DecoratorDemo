using Decorator_Demo.Application.Interfaces.UseCases;
using Decorator_Demo.Domain.Entities;
using Decorator_Demo.Domain.Interfaces.Repositories;

namespace Decorator_Demo.Application.UseCases;

public class GetAllProductsUseCase(IProductRepository repository) : IGetAllProductsUseCase
{
    public async Task<IEnumerable<Product>> Execute() => await repository.GetAllAsync();
}