using Decorator_Demo.Application.Interfaces.UseCases;
using Decorator_Demo.Domain.Entities;
using Decorator_Demo.Domain.Interfaces.Repositories;

namespace Decorator_Demo.Application.UseCases;

public class AddProductUseCase(IProductRepository repository) : IAddProductUseCase
{
    public void Execute(Product product) => repository.Add(product);
}