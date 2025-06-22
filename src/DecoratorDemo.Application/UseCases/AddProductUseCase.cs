using DecoratorDemo.Domain.Entities;
using DecoratorDemo.Domain.Interfaces.Repositories;
using DecoratorDemo.Application.Interfaces.UseCases;

namespace DecoratorDemo.Application.UseCases;

public class AddProductUseCase(IProductRepository repository) : IAddProductUseCase
{
    public void Execute(Product product) => repository.Add(product);
}