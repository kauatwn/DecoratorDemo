using Decorator_Demo.Domain.Entities;

namespace Decorator_Demo.Application.Interfaces.UseCases;

public interface IAddProductUseCase
{
    void Execute(Product product);
}